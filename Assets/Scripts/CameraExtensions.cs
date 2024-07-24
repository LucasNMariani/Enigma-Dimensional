using UnityEngine;

public static class CameraExtensions
{
    /// <summary>
    /// Muestra la Layer por numero
    /// </summary>
    /// <param name="cam"></param>
    /// <param name="layerMask"></param>
    public static void LayerCullingShow(this Camera cam, int layerMask)
    {
        cam.cullingMask |= layerMask;
    }

    /// <summary>
    /// Muestra la Layer por nombre
    /// </summary>
    /// <param name="cam"></param>
    /// <param name="layer"></param>
    public static void LayerCullingShow(this Camera cam, string layer)
    {
        LayerCullingShow(cam, 1 << LayerMask.NameToLayer(layer));
    }

    /// <summary>
    /// Oculta la Layer por numero
    /// </summary>
    /// <param name="cam"></param>
    /// <param name="layerMask"></param>
    public static void LayerCullingHide(this Camera cam, int layerMask)
    {
        cam.cullingMask &= ~layerMask;
    }

    /// <summary>
    /// Oculta la Layer por nombre
    /// </summary>
    /// <param name="cam"></param>
    /// <param name="layer"></param>
    public static void LayerCullingHide(this Camera cam, string layer)
    {
        LayerCullingHide(cam, 1 << LayerMask.NameToLayer(layer));
    }

    /// <summary>
    /// Alterna la Layer por numero de mascara
    /// </summary>
    /// <param name="cam"></param>
    /// <param name="layerMask"></param>
    public static void LayerCullingToggle(this Camera cam, int layerMask)
    {
        cam.cullingMask ^= layerMask;
    }

    /// <summary>
    /// Alterna la Layer por nombre
    /// </summary>
    /// <param name="cam"></param>
    /// <param name="layer"></param>
    public static void LayerCullingToggle(this Camera cam, string layer)
    {
        LayerCullingToggle(cam, 1 << LayerMask.NameToLayer(layer));
    }


   
}
