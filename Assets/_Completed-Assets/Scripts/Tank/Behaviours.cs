using UnityEngine;
using NPBehave;
using System.Collections.Generic;

namespace Complete
{
    /*
    Example behaviour trees for the Tank AI.  This is partial definition:
    the core AI code is defined in TankAI.cs.

    Use this file to specifiy your new behaviour tree.
     */
    public partial class TankAI : MonoBehaviour
    {
        private Root CreateBehaviourTree() {

            switch (m_Behaviour) {

                case 1:
                    return DeadlyBehaviour();

                case 2:
                    return FrightenedBehaviour();

                case 3:
                    return CrazyBehaviour();

                default:
                    return new Root (new Action(()=> Turn(0.1f)));
            }
        }

        /* Actions */

        private Node StopTurning() {
            return new Action(() => Turn(0));
        }

        private Node RandomFire() {
            return new Action(() => Fire(UnityEngine.Random.Range(0.0f, 1.0f)));
        }

        //Randomly picks a number between -1 and 1 and moves accordingly
        private Node RandomMove()
        {
            return new Action(() => Move(UnityEngine.Random.Range(-1f, 1f)));
        }

        /* Behaviour trees */

        //(1.Deadly) Tracks the opponent, moves towards it and constantly shoots
        private Root DeadlyBehaviour()
        {
            return new Root(
                new Service(0.2f, UpdatePerception,
                    new Selector(
                        //Used this from the original Behaviour.cs
                        new BlackboardCondition("targetOffCentre",
                                                Operator.IS_SMALLER_OR_EQUAL, 0.1f,
                                                Stops.IMMEDIATE_RESTART,
                            // Move towards opponent and turn
                            new Sequence(
                                new Action(() => Move(0.4f)),
                                new Action(() => Turn(1f))
                            )),
                        new BlackboardCondition("targetOnRight",
                                                Operator.IS_EQUAL, true,
                                                Stops.IMMEDIATE_RESTART,
                            // Turn right toward target
                            new Action(() => Turn(1f))
                            ),
                        //When the distance between the two players is less or equal to the given value, run this BlackboardCondition and the subsequent Sequence node
                        new BlackboardCondition("agentDistance",
                                                 Operator.IS_SMALLER_OR_EQUAL, 15f,
                                                 Stops.IMMEDIATE_RESTART,
                            new Sequence(
                                StopTurning(),
                                new Action (() => Fire(-1f))
                            )
                            // Turn left toward target
                            ),
                           new Action(() => Turn(-1f))
                            
                    )
                )
            );

        }

        //(2.Fightened) Tracks the opponent, moves away from it, doesn't shoot
        private Root FrightenedBehaviour()
        {
            return new Root(
 
                new Service(0.2f, UpdatePerception,
                    new Selector(
                        new BlackboardCondition("agentDistance",
                                                 Operator.IS_SMALLER_OR_EQUAL, 20f,
                                                 Stops.IMMEDIATE_RESTART,
                                                 new Sequence(
                                             new Action(() => Move(-0.5f)),
                                             new Action(() => Turn(0.5f))
                                                )),
                        new BlackboardCondition("agentDistance",
                                                 Operator.IS_GREATER_OR_EQUAL, 30f,
                                                 Stops.IMMEDIATE_RESTART,
                                                 new Sequence(
                                             new Action(() => Move(0.1f)),
                                             new Action(() => Turn(-0.5f))
                                                )),

                        //Used this code snippet from the original Behaviour.cs
                        new BlackboardCondition("targetOnRight",
                                                Operator.IS_EQUAL, true,
                                                Stops.IMMEDIATE_RESTART,
                            // Turn right toward target
                            new Action(() => Turn(1f))),
                            // Turn left toward target
                            new Action(() => Turn(-1f))

                    )
                )
                
            );

        }

        //It keeps spinning and randomly fires in different direction, but as soon as you come close, it does something unexpected
        private Root CrazyBehaviour()
        {
            return new Root(
                     new Service(0.2f, UpdatePerception,
                         new Selector(
                             new BlackboardCondition("agentDistance", Operator.IS_SMALLER_OR_EQUAL, 15f, Stops.IMMEDIATE_RESTART,

                                 // the player is in our range of 15f
                                 new Sequence(
                                     new Action(() => Turn(0.5f)),
                                     new Wait(0.5f),
                                     new Action(() => Move(0.6f))
                                 )
                             ),
                             new BlackboardCondition("targetInFront", Operator.IS_EQUAL, true, Stops.IMMEDIATE_RESTART,

                                 // the player is in our range of 15f
                                 new Sequence(
                                     new Action(() => Turn(1f)),
                                     new Wait(0.2f),
                                     RandomFire()
                                
                                 )
                             )
                         )
                     )
                 );
        }

        private void UpdatePerception() {
            Vector3 targetPos = TargetTransform().position;
            Vector3 localPos = this.transform.InverseTransformPoint(targetPos);
            Vector3 heading = localPos.normalized;
            blackboard["targetDistance"] = localPos.magnitude;
            blackboard["targetInFront"] = heading.z > 0;
            blackboard["targetOnRight"] = heading.x > 0;
            blackboard["targetOffCentre"] = Mathf.Abs(heading.x);
            blackboard["agentDistance"] = localPos.magnitude; 
        }

    }
}