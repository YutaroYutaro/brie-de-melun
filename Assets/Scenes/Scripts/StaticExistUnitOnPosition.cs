using UnityEngine;

static class StaticExistUnitOnPosition
{
    public static bool ExistUnitOnPosition(
        Transform children,
        int posX,
        int posZ,
        bool active)
    {
        if (active)
        {
            foreach (Transform child in children)
            {
                if (child.GetComponent<UnitOwnIntPosition>().PosX == posX &&
                    child.GetComponent<UnitOwnIntPosition>().PosZ == posZ &&
                    child.gameObject.activeSelf)
                {
                    return true;
                }
            }
        }
        else
        {
            foreach (Transform child in children)
            {
                if (child.GetComponent<UnitOwnIntPosition>().PosX == posX &&
                    child.GetComponent<UnitOwnIntPosition>().PosZ == posZ &&
                    !child.gameObject.activeSelf)
                {
                    return true;
                }
            }
        }

        return false;
    }
}