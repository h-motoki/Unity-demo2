using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManagerController : MonoBehaviour {

	//BGMがフェードするのにかかる時間
	public const float BGM_FADE_SPEED_RATE_HIGH = 0.9f;
	public const float BGM_FADE_SPEED_RATE_LOW = 0.3f;
	private float _bgmFadeSpeedRate = BGM_FADE_SPEED_RATE_HIGH;

	//次流すBGM名、SE名
	private string _nextBGMName;
	private string _nextSEName;

	//BGMをフェードアウト中か
	private bool _isFadeOut = false;
	
	//BGM用、SE用に分けてオーディオソースを持つ
	public AudioSource AttachBGMSource, AttachSESource;
	
	//全Audioを保持
	private Dictionary<string, AudioClip> _bgmDic, _seDic;

	private void Awake ()
	{
		//リソースフォルダから全SE&BGMのファイルを読み込みセット
		_bgmDic = new Dictionary<string, AudioClip> ();
		_seDic  = new Dictionary<string, AudioClip> ();
		
		object[] bgmList = Resources.LoadAll ("Audio/BGM");
		object[] seList  = Resources.LoadAll ("Audio/SE");
		
		foreach (AudioClip bgm in bgmList) {
			_bgmDic [bgm.name] = bgm;
		}
		foreach (AudioClip se in seList) {
			_seDic [se.name] = se;
		}
	}

	/// <summary>
	/// 指定したファイル名のSEを流す。第二引数のdelayに指定した時間だけ再生までの間隔を空ける
	/// </summary>
	public void PlaySE (string seName, float delay = 0.0f)
	{
		if (!_seDic.ContainsKey (seName)) {
			Debug.Log (seName + "という名前のSEがありません");
			return;
		}
		
		_nextSEName = seName;
		Invoke ("DelayPlaySE", delay);
	}
	
	private void DelayPlaySE ()
	{
		AttachSESource.PlayOneShot (_seDic [_nextSEName] as AudioClip);
	}

	//=================================================================================
	//BGM
	//=================================================================================
	
	/// <summary>
	/// 指定したファイル名のBGMを流す。ただし既に流れている場合は前の曲をフェードアウトさせてから。
	/// 第二引数のfadeSpeedRateに指定した割合でフェードアウトするスピードが変わる
	/// </summary>
	public void PlayBGM (string bgmName, float fadeSpeedRate = BGM_FADE_SPEED_RATE_HIGH)
	{
		if (!_bgmDic.ContainsKey (bgmName)) {
			Debug.Log (bgmName + "という名前のBGMがありません");
			return;
		}
		
		//現在BGMが流れていない時はそのまま流す
		if (!AttachBGMSource.isPlaying) {
			_nextBGMName = "";
			AttachBGMSource.clip = _bgmDic [bgmName] as AudioClip;
			AttachBGMSource.loop = true;
			AttachBGMSource.Play ();
		}
		//違うBGMが流れている時は、流れているBGMをフェードアウトさせてから次を流す。同じBGMが流れている時はスルー
		else if (AttachBGMSource.clip.name != bgmName) {
			_nextBGMName = bgmName;
			FadeOutBGM (fadeSpeedRate);
		}
		
	}
	
	/// <summary>
	/// 現在流れている曲をフェードアウトさせる
	/// fadeSpeedRateに指定した割合でフェードアウトするスピードが変わる
	/// </summary>
	public void FadeOutBGM (float fadeSpeedRate = BGM_FADE_SPEED_RATE_LOW)
	{
		_bgmFadeSpeedRate = fadeSpeedRate;
		_isFadeOut = true;
	}

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		if (!_isFadeOut) {
			return;
		}
		
		//徐々にボリュームを下げていき、ボリュームが0になったらボリュームを戻し次の曲を流す
		AttachBGMSource.volume -= Time.deltaTime * _bgmFadeSpeedRate;
		if (AttachBGMSource.volume <= 0) {
			AttachBGMSource.Stop ();
			_isFadeOut = false;
			
			if (!string.IsNullOrEmpty (_nextBGMName)) {
				PlayBGM (_nextBGMName);
			}
		}
	}
}
