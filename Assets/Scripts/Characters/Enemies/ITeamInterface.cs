using UnityEngine;

namespace MonsterExterminator.Characters.Enemies
{
    public interface ITeamInterface
    {
        public int GetTeamID() => -1;

        public TeamRelation GetRelationTowards(GameObject other)
        {
            ITeamInterface otherTeamInterface = other.GetComponent<ITeamInterface>();

            if (otherTeamInterface == null)
                return TeamRelation.Neutral;

            if (otherTeamInterface.GetTeamID() == GetTeamID())
                return TeamRelation.Friendly;

            if (otherTeamInterface.GetTeamID() == -1 || GetTeamID() == -1)
                return TeamRelation.Neutral;

            return TeamRelation.Enemy;
        }
    }
}