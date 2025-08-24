using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//Для задания 5: А и Б
namespace _Tasks
{
    public class Closure : MonoBehaviour
    {
        [SerializeField] private Button[] _buttons;

        private void Start()
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                int index = i;
                _buttons[i].onClick.AddListener((() => Debug.Log(index)));
            }

            ActionOnTask task = Task1;
            
            StartCoroutine(ScheduleAction(2, task));

            task = Task2;
        }

        private IEnumerator ScheduleAction(float delay, ActionOnTask task)
        {
            yield return new WaitForSeconds(delay);
            task?.Invoke();
        }

        private void Task1()
        {
            Debug.Log("Task1");
        }

        private void Task2()
        {
            Debug.Log("Task2");
        }
    }

    delegate void ActionOnTask();
}
