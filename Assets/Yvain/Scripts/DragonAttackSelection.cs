using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class DragonAttackSelection : MonoBehaviour
{
    public GameObject dragonFlame;
    public GameObject dragonThunder;
    public GameObject dragonShockwave;
    [SerializeField] private GameObject[] _dragonAttacks;
    //int _selectedAttack = 1;
    //public int SelectedAttack
    //{
    //    get { return _selectedAttack; }
    //    set { _selectedAttack = value; }
    //}

    //bool _started = false;
    //public int dragonAttackIndex = 1;

    //public void AddRightChoice()
    //{
    //    dragonAttackIndex++;
    //}
    //public void AddLeftChoice()
    //{
    //    dragonAttackIndex--;
    //}
    //public void Select()
    //{
    //    started = true;
    //}
    //private void Start()
    //{
    //    dragonFlame.SetActive(false);
    //    dragonThunder.SetActive(false);
    //    dragonShockwave.SetActive(false);
    //}
    //void Update()
    //{
    //    //if (FindAnyObjectByType<LevelManager>().IsIngame)
    //    //{
    //    //    _dragonAttacks[dragonAttackIndex].SetActive(true);
    //        //if (Input.GetMouseButton(0) && choice == 1)
    //        //{
    //        //    dragonFlame.SetActive(true);
    //        //}
    //        //else if(Input.GetMouseButton(0) && choice == 2)
    //        //{
    //        //    dragonThunder.SetActive(true);
    //        //}
    //        //else if(Input.GetMouseButton(0) && choice == 3)
    //        //{
    //        //    dragonShockwave.SetActive(true);
    //        //}
    //        //else
    //        //{
    //        //    dragonFlame.SetActive(false);
    //        //    dragonThunder.SetActive(false);
    //        //    dragonShockwave.SetActive(false);
    //        //}
    //    }
    //}
}
