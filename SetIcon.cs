using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Xml;
using System.Drawing;
using System.Runtime.InteropServices;

public class SetIcon : MonoBehaviour{


	private SpriteRenderer spriteRenderer;

	void Start ()
	{
		SetIconTexture ();
	}

	private static readonly string USERS_SHOW_URL = 
		"http://api.twitter.com/1.1/users/show.xml";
	



	private void SetIconTexture()
	{
		// 画像を取得するID
		string screen_name = "16nox"; // ←画像を取得したいIDを入れてください

		// 画像を取得
		Bitmap image = GetProfileImage(screen_name);
		Texture2D tex = new Texture2D(64, 64);
		byte[] iconByteArray = imageToByteArray (image);
		tex.LoadImage(iconByteArray);
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		spriteRenderer.sprite =Sprite.Create(tex, new Rect(0,0, 64, 64), new Vector2(0,0));
	}
	public Bitmap GetProfileImage(string screen_name)
	{
		// Users情報を取得

		WebRequest api_req = WebRequest.Create(
			USERS_SHOW_URL + "?screen_name=" + screen_name);
		api_req.UseDefaultCredentials = true;
		WebResponse api_res = api_req.GetResponse();
		StreamReader api_sr = new StreamReader(
			api_res.GetResponseStream());

		// XmlDocumentに読み込ませる
		XmlDocument xml = new XmlDocument();
		xml.LoadXml(api_sr.ReadToEnd());

		// profile_image_urlを取得
		XmlNode node = xml.SelectSingleNode("/user/profile_image_url");
		string profile_image_url = node.InnerText;

		// 画像を取得
		WebRequest image_req = WebRequest.Create(profile_image_url);
		image_req.UseDefaultCredentials = true;
		WebResponse image_res = image_req.GetResponse();

		// BITMAPに変換
		Stream stream = image_res.GetResponseStream();
		Bitmap bitmap = new Bitmap(stream);

		// 後始末
		stream.Close();
		image_res.Close();
		api_res.Close();


		// BITMAPを返却
		return bitmap;
	}
	public byte[] imageToByteArray(System.Drawing.Image imageIn) {
		var o = System.Drawing.GraphicsUnit.Pixel;
		RectangleF r1 =  imageIn.GetBounds(ref o);
		Rectangle r2 = new Rectangle((int)r1.X, (int)r1.Y, (int)r1.Width, (int)r1.Height);
		System.Drawing.Imaging.BitmapData omg = ((Bitmap)imageIn).LockBits(r2, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
		byte[] rgbValues = new byte[r2.Width*r2.Height*4];
		Marshal.Copy((IntPtr)omg.Scan0,  rgbValues,0, rgbValues.Length);
		((Bitmap)imageIn).UnlockBits(omg);
		return rgbValues;        
	}
}
