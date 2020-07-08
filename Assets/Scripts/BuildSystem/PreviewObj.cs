using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewObj : MonoBehaviour
{

    [SerializeField] GameObject child = null;
    private List<GameObject> obj = new List<GameObject>();//list of all buildings and walls the preview collided with
    private List<GroundCube> cubes = new List<GroundCube>();//list of all the ground cubes the preview is sitting ontop of
    public List<Building> buildedObjects = new List<Building>();
    public Material goodMat;//good material (green)
    public Material badMat;//bad material (red)
    public GameObject prefab;//actual prefab

    private MeshRenderer myRend;//to change the material on runtime
    private bool canBuild = false;
    private SoundManager soundManager;
    private CameraCOntrollerv2 cameraController;


    private void Awake()
    {
        //SaveLoad.Saving += Save;
        //SaveLoad.Loading += Load;
    }
    private void OnEnable()
    {
        soundManager = FindObjectOfType<SoundManager>();
        cameraController = FindObjectOfType<CameraCOntrollerv2>();
        cameraController.gameObject.SetActive(false);//disabling camera controller while preview of building is active 
    }
    private void Start()
    {
        
        myRend = child.GetComponent<MeshRenderer>();
        ChangeColor();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            DragWithTouch();
        }
    }
    private void OnDestroy()//
    {
        cameraController.gameObject.SetActive(true);//turning camera controller back on
    }

    private void DragWithTouch()
    {
        Touch touch = Input.GetTouch(0); // get first touch since touch count is greater than zero

        if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
        {
            // get the touch position from the screen touch to world point
            Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
            // lerp and set the position of the current object to that of the touch, but smoothly over time.
            transform.position = Vector3.Lerp(transform.position, touchedPos, Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Building") || other.CompareTag("Wall"))//hit  building or  wall(environment obstacle)
        {
            obj.Add(other.gameObject);//add it in the obj list
        }

        if (other.CompareTag("GroundCube"))//hit a ground cube
        {
            GroundCube gc = other.GetComponent<GroundCube>();//get the ground cube script that is sitting on this particular gameobject
            cubes.Add(gc);//add it to the ground cubes list
            gc.HandleSelection();//toggle the selection color of this particular ground cube
        }
        ChangeColor();//<----no check if the color should be green or red
    }

    
    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Building") || other.CompareTag("Wall"))
        {
            obj.Remove(other.gameObject);// removing it from the list
        }

        if (other.CompareTag("GroundCube"))
        {
            GroundCube gc = other.GetComponent<GroundCube>();
            cubes.Remove(gc);//removing it from the list
            gc.HandleSelection();
        }
        ChangeColor();
    }

    //Only concerned about the obj list (Buildings and Walls) if there is nothing in that list then we can build, if there is even 
    // 1 thing in the list then you cant build
    private void ChangeColor()
    {
        if(obj.Count == 0)//nothing in the list
        {
            myRend.material = goodMat;// change color to green
            canBuild = true;//you can build
        }
        else//something is in the list
        {
            myRend.material = badMat;//change to red
            canBuild = false;//cant build
        }
    }

    public void Build()//building object from shop
    {
       
        for (int i = 0; i < cubes.Count; i++)//loop through all ground cubes and change their selection
        {
            cubes[i].HandleSelection();
        }

       GameObject go= Instantiate(prefab, transform.position, transform.rotation);//spawn in the prefab(Actual Building this preview represents)
        soundManager.PlayBuildSound();
        buildedObjects.Add(go.GetComponent<BuildingFunctionallity>().building);
        Destroy(gameObject);//destroy the preview

    }

    public void Build(Building _building)//build object wich was already created ( moving it up essentially)
    {
       
        for (int i = 0; i < cubes.Count; i++)//loop through all ground cubes and change their selection
        {
            cubes[i].HandleSelection();
        }
        GameObject newBuilding = Instantiate(prefab, transform.position, transform.rotation);//cash build in the new reference
        soundManager.PlayBuildSound();
        newBuilding.GetComponent<BuildingFunctionallity>().building = _building;//pass into it info about our existing Gamobject(lvl, etc)
       if(buildedObjects.Contains(_building))
        {
            buildedObjects.Remove(_building);
            buildedObjects.Add(newBuilding.GetComponent<BuildingFunctionallity>().building);
        }
       
        Destroy(gameObject);
    }

    public bool CanBuild()//just returns the canBuild bool=>this is so it cant accidently be changed by another script
    {
        return canBuild;
    }

   //void Save()
   // {
   //     SaveLoad.Save(buildedObjects, "Buildings");
   // }

   // void Load()
   // {
   //     if (SaveLoad.SaveAlreadyExists("Buildings"))
   //         buildedObjects = SaveLoad.Load<List<Building>>("grid");
   // }

}
