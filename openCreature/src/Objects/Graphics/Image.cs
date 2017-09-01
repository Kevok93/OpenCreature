using System;

namespace opencreature {
public class Image {
	public byte[] image;
	private Image() {}
	public Image(string path) {
		image = System.IO.File.ReadAllBytes(path);
	}
}
}
