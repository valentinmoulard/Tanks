using UnityEngine;

public class UIDirectionControl : MonoBehaviour
{
    //garde la bare de vie dans la meme rotation que le tank
    public bool m_UseRelativeRotation = true;  


    private Quaternion m_RelativeRotation;     


    private void Start()
    {
        m_RelativeRotation = transform.parent.localRotation;
    }


    private void Update()
    {
        if (m_UseRelativeRotation)
            transform.rotation = m_RelativeRotation;
    }
}
