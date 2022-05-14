using UnityEngine;

public abstract class BaseTester : MonoBehaviour
{
    [SerializeField] private KeyCode TestKey1;
    [SerializeField] private KeyCode TestKey2;
    [SerializeField] private KeyCode TestKey3;

    private void LateUpdate()
    {
        if(Input.GetKeyDown(TestKey1))
        {
            Test1();
        }
        if (Input.GetKeyDown(TestKey2))
        {
            Test2();
        }
        if (Input.GetKeyDown(TestKey3))
        {
            Test3();
        }
    }

    public abstract void Test1();
    public abstract void Test2();
    public abstract void Test3();
}