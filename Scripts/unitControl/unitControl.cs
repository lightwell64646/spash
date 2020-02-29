using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitControl : MonoBehaviour
{

    public unitInterface thisUnit = null;
    public int maxQueuedCommands = 5;

    private Queue<command> commandQueue;

    // Start is called before the first frame update
    void Start()
    {
        if (thisUnit == null) thisUnit = GetComponent<unitInterface>();
    }

    public bool addCommand(command com)
    {
        if (commandQueue.Count < maxQueuedCommands)
        {
            commandQueue.Enqueue(com);
            com.addCommand(thisUnit);
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (commandQueue.Count != 0)
        {
            if (commandQueue.Peek().execute())
            {
                commandQueue.Dequeue();
                if (commandQueue.Count != 0)
                    if (!commandQueue.Peek().startup()) commandQueue.Dequeue();
            }
        }
    }
}
