using UnityEngine;

public class BarScript : MonoBehaviour {

	[SerializeField]
	private float fillAmount = 0;

	[SerializeField]
	UnityEngine.UI.Image content;

	[SerializeField]
	UnityEngine.UI.Text valueText;

	[SerializeField]
	float lerpSpeed;

	[SerializeField]
	UnityEngine.UI.Button btnReward;

	public float MaxValue { get; set; }

	public bool isChange;

	public float Value{
		set{
			//MaxValue = 100;
			if (valueText != null)
				valueText.text = value + "/" + MaxValue;
			//Debug.Log ("value: " +value + " maxVal: "+MaxValue + " "  + Map (value, 0, MaxValue, 0, 1));
			fillAmount = Map (value, 0, MaxValue, 0, 1);
		}
		//get{return MaxValue;}
	}

	// Update is called once per frame
	void Update () {
		//Debug.Log (Value);
		HandleBar ();
	}

	void HandleBar(){
		if (isChange) {
			if (fillAmount > content.fillAmount) {
				content.fillAmount = Mathf.Lerp (content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
			}
			if (fillAmount < content.fillAmount) {
				content.fillAmount = Mathf.Lerp (content.fillAmount, 1, Time.deltaTime * lerpSpeed);
				if (content.fillAmount >= 0.95f && content.fillAmount <= 1f) {
					content.fillAmount = Mathf.Lerp (0, fillAmount, Time.deltaTime * lerpSpeed);
				}
			}

			if (fillAmount == 1) {
				btnReward.enabled = true;
			}

			if (fillAmount == content.fillAmount)
				isChange = false;
		} else {
			content.fillAmount = fillAmount;
		}
		//Debug.Log (MaxValue);

		//Debug.Log ("fillAmount: " + fillAmount + "  content.fillAmount: " + content.fillAmount);
	}

	float Map(float value, float inMin, float inMax, float outMin, float outMax){
		//Debug.Log ((value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin);
		return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
		//(value, 0, MaxValue, 0, 1);

	}
}
