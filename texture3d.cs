using System;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;

namespace Template_P3 {

public class Texture3d
{
	// data members
	public int id;

	// constructor
	public Texture3d( string filename )
	{
		if (String.IsNullOrEmpty( filename )) throw new ArgumentException( filename );
		id = GL.GenTexture();
		GL.BindTexture( TextureTarget.Texture3D, id );
 		// We will not upload mipmaps, so disable mipmapping (otherwise the texture will not appear).
		// We can use GL.GenerateMipmaps() or GL.Ext.GenerateMipmaps() to create
		// mipmaps automatically. In that case, use TextureMinFilter.LinearMipmapLinear to enable them.
		GL.TexParameter( TextureTarget.Texture3D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear );
		GL.TexParameter( TextureTarget.Texture3D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear );
		Bitmap bmp = new Bitmap( filename );
		BitmapData bmp_data = bmp.LockBits( new Rectangle( 0, 0, bmp.Width, bmp.Height ), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb );
 		GL.TexImage3D( TextureTarget.Texture3D, 0, PixelInternalFormat.Rgba, bmp.Height, bmp.Height, bmp.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmp_data.Scan0 );
		bmp.UnlockBits( bmp_data );
	}
}

} // namespace Template_P3
