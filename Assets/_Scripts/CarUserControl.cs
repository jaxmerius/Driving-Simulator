using System;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;


    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use
        public GameObject[] testTriggerGameObjects;
        public String[] feedbackStrings;
        public int[] speedLimits;
        public Transform[] checkPoints;
        public String[] rallyQueueStrings;
        private int testIndex = 0;

        public Image needle;
        public Image shifter;
        public Sprite drive;
        public Sprite reverse;
        public Sprite slowDown;
        public Sprite currSprite;
        public float speedLimit = 20f;
        private bool aboveSpeed = false;
        public RallyAnimation speedingQueue;
        private ScoreCard scoreCard;

        public int[] testFailures;
        public bool[] passedTests;

        public void Start()
        {
            scoreCard = FindObjectOfType<ScoreCard>();
        }

        private void Awake()
        {
            /* foreach (GameObject trigger in testTriggerGameObjects) {
                trigger.SetActive(false);
            }
            testTriggerGameObjects[testIndex].SetActive(true);*/
            // get the car controller
            m_Car = GetComponent<CarController>();
        }

        public void TestPassed() {
            Debug.Log("test " + testIndex + " passed");
            passedTests[testIndex] = true;
            testTriggerGameObjects[testIndex].SetActive(false);
            testIndex++;
            testTriggerGameObjects[testIndex].SetActive(true);
        }

        public void TestFailed() {
            testFailures[testIndex]++; 
            this.transform.position = checkPoints[testIndex].position;
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.R) || CrossPlatformInputManager.GetButtonDown("Reverse"))
            {//m_Car.CurrentSpeed == 0f && 
                m_Car.SwitchReverseBool();
            }

            if (m_Car.GetInReverse()) {
                shifter.sprite = reverse;
            } else {
                shifter.sprite = drive;
            }

            if (aboveSpeed)
            {
                scoreCard.TimeAboveSpeed(Time.deltaTime);
            } 
        }


        private void FixedUpdate()
        {

            float rotationDegree = (95.0f - m_Car.CurrentSpeed * 2.38f);
            if (m_Car.CurrentSpeed > speedLimit)
            {
                aboveSpeed = true;
                speedingQueue._rallyImage.color = Color.white;
                speedingQueue._rallyImage.sprite = slowDown;
            }
            else
            {
                aboveSpeed = false;
                speedingQueue._rallyImage.color = Color.clear;
                speedingQueue._rallyImage.sprite = speedingQueue.curSprite;
            }
            needle.transform.rotation = Quaternion.Euler(0, 0, rotationDegree);
            if (Math.Round(m_Car.CurrentSpeed) > speedLimits[testIndex]) {
            } else {
            }
            if (m_Car.GetInReverse()) {
                //Debug.Log("in reverse");
                //Debug.Log("Speed: " + m_Car.CurrentSpeed + " Accel: " + m_Car.AccelInput + " Break: " + m_Car.BrakeInput);

            } else {
                //Debug.Log("not in reverse");
            }
            //Debug.Log("Speed: " + m_Car.CurrentSpeed + " Accel: " + m_Car.AccelInput + " Break: " + m_Car.BrakeInput);
            // pass the input to the car!
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Brake");
            m_Car.Move(h, v, v, handbrake);
            
#else
            m_Car.Move(h, v, v, 0f);
#endif
        }
    }

