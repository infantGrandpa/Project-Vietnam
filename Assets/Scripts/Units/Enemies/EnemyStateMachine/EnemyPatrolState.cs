using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProjectVietnam
{
    public class EnemyPatrolState : EnemyStateBase
    {

        private float secsToPatrol;
        private float secsPatrolling;
        private List<Vector2> patrolPoints = new();
        private List<int> validPatrolIndexes = new();
        private int lastChosenIndex = -1;
        private readonly float fuzzyRadiusMultiplier = 1 / 3;

        public EnemyPatrolState(EnemyBehaviour newEnemyBehaviour)
        {
            enemyBehaviour = newEnemyBehaviour;
        }

        public override void EnterState()
        {
            PopulatePatrolPoints();
            Vector3 patrolPosition = GetPatrolPosition();
            enemyBehaviour.SetNewDestination(patrolPosition);
            StartPatrolCountdown();
        }

        public override void ExitState()
        {
            DebugHelper.Log("Exiting patrol state.");
        }

        public override bool IsEqualToCommandType(EnemyCommandType enemyCommandType)
        {
            return enemyCommandType == EnemyCommandType.patrol;
        }

        public override bool IsStateComplete()
        {
            return secsPatrolling >= secsToPatrol;
        }

        public override void UpdateState()
        {
            secsPatrolling += Time.deltaTime;

            if (!enemyBehaviour.HasArrivedAtDestination())
            {
                return;
            }

            Vector3 patrolPosition = GetPatrolPosition();
            enemyBehaviour.SetNewDestination(patrolPosition);
        }

        private Vector3 GetPatrolPosition()
        {
            RemoveLastChosenIndexFromValidPatrols();

            int newlyChosenIndex = GetRandomPatrolIndex();
            Vector2 patrolDestination = ApplyPositionFuzziness(patrolPoints[newlyChosenIndex]);

            RefreshValidPatrolIndexes(newlyChosenIndex);

            return patrolDestination;
        }

        private void RemoveLastChosenIndexFromValidPatrols()
        {
            if (lastChosenIndex == -1) return;
            validPatrolIndexes.Remove(lastChosenIndex);
        }

        private int GetRandomPatrolIndex()
        {
            return validPatrolIndexes[Random.Range(0, validPatrolIndexes.Count)];
        }

        private void RefreshValidPatrolIndexes(int newlyChosenIndex)
        {
            if (lastChosenIndex != -1)
            {
                validPatrolIndexes.Add(lastChosenIndex);
            }

            lastChosenIndex = newlyChosenIndex;
        }

        private void StartPatrolCountdown()
        {
            secsToPatrol = EnemyStatePlanner.Instance.secsToPatrolBeforeNewCommand;
            secsPatrolling = 0;
        }

        private void PopulatePatrolPoints()
        {
            float radius = EnemyStatePlanner.Instance.patrolRadius;
            Vector2 center = (Vector2)targetPosition;
            float halfRadius = radius / 2;

            // Northeast
            patrolPoints.Add(new Vector2(center.x + halfRadius, center.y + halfRadius));
            // Northwest
            patrolPoints.Add(new Vector2(center.x - halfRadius, center.y + halfRadius));
            // Southeast
            patrolPoints.Add(new Vector2(center.x + halfRadius, center.y - halfRadius));
            // Southwest
            patrolPoints.Add(new Vector2(center.x - halfRadius, center.y - halfRadius));

            validPatrolIndexes = Enumerable.Range(0, patrolPoints.Count).ToList();

        }

        private Vector2 ApplyPositionFuzziness(Vector2 startingPosition)
        {
            Vector2 fuzzyPosition = Random.insideUnitCircle;
            fuzzyPosition *= EnemyStatePlanner.Instance.patrolRadius * fuzzyRadiusMultiplier;
            fuzzyPosition += startingPosition;

            return fuzzyPosition;
        }

    }
}
