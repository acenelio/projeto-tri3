using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelCompleteCtrl : MonoBehaviour {

	public Button btnNext;
	public Sprite goldenStar;
	public Image Star1;
	public Image Star2;
	public Image Star3;
	public Text txtScore;
	public int score;
	public int scoreForOneStar;
	public int scoreForTwoStars;
	public int scoreForThreeStars;
	public int scoreForNextLevel;

	public float animStartDelay;
	public float animDelay;


	void Start () {
		txtScore.text = score.ToString();

		Invoke("IniciarMostrarEstrelas", animStartDelay);
	}

	void IniciarMostrarEstrelas() {
		StartCoroutine("MostrarEstrelas");
	}

	IEnumerator MostrarEstrelas() {
		if (score >= scoreForOneStar) {
			ExecutarAnimacao(Star1);
			yield return new WaitForSeconds(animDelay);
			if (score >= scoreForTwoStars) {
				ExecutarAnimacao(Star2);
				yield return new WaitForSeconds(animDelay);
				if (score >= scoreForThreeStars) {
					ExecutarAnimacao(Star3);
					yield return new WaitForSeconds(animDelay);
				}
			}
		}

		if (score >= scoreForNextLevel) {
			btnNext.interactable = true;
			Plim(btnNext.gameObject);
		}
	}

	void ExecutarAnimacao(Image starImg) {
		// Aumentar tamanho da estrela
		starImg.rectTransform.sizeDelta = new Vector2(150f, 150f);

		// Mostrar a estrela dourada
		starImg.sprite = goldenStar;

		// Reduzir o tamanho da estrela
		RectTransform t = starImg.rectTransform;
		t.DOSizeDelta(new Vector2(100f, 100f), 0.5f);

		Plim(starImg.gameObject);
	}

	void Plim(GameObject obj) {
		SFXManager.instance.ShowStarParticles(obj);
		AudioManager.instance.PlayStarSound(obj);
	}
}
