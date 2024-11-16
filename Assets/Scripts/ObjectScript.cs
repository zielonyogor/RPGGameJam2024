using UnityEngine;

public enum Eras
{
    Past,
    Present,
    Future
}

public class ObjectScript : MonoBehaviour
{
    public int objectID = 0;

    public Eras originalEra = Eras.Past;
    public Eras currentEra;

}
