Rasterizer

Created by:
Bernd van den Hoek - 5895391
Maaike Galama - 5987857
Wilmer Zwietering - 5954312

Implemented bonusses:
Frustum culling to the scene graph render method - The implementation consists of three parts: 
1 Constructing a bounding sphere around the object, in Node.GetBoundingSphere().
2 Checking whether the sphere is behind the planes of the view frustrum, in Node.IsInViewFrustrum().
3 Constructing the view frustrum planes themselves. this happens in SceneGraph.GetSceneBoundingBox().
Vignetting and chromatic aberration - Found in the shaders/fs_post.glsl file. First we find the distance of the pixel to the center. This distance is applied to the colorOffset variable, which offsets the color to create chromatic abberration. This makes the chromatic abberation stronger around the edges. Vignetting is done by darkening the pixels based on the same distance variable. A bigger distance means more darkening.
Generic colorization using color cube - Found in the shaders/fs_post.glsl file. The color is modified using a color lookup texture, which can be found in assets/ColorLookupTexture.jpg. The modification is done by blending the texture color with the color of the rendered image.

Used resources:
https://neokabuto.blogspot.nl/2015/12/opentk-tutorial-8-part-2-adding.html To learn about Phong shading. We did not use the code of this blog, but we can not deny that we looked carefully at the implementation.
http://neokabuto.blogspot.nl/2014/01/opentk-tutorial-5-basic-camera.html Used to create the camera class

Controls:
w = forwards
s = backwards
a = left
d = right
e = up
q = down
m = toggle mousecontrol
