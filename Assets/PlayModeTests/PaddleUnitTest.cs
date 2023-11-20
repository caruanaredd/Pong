using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Pong;
using UnityEngine;
using UnityEngine.TestTools;

public class PaddleUnitTest
{
    // This is just so we can see the test running.
    private Camera _mainCamera;

    private void CreateCamera()
    {
        // Just to prevent more than one camera
        // to be on the same scene
        if (_mainCamera != null)
        {
            return;
        }

        GameObject cameraObject = new GameObject("MainCamera");
        cameraObject.transform.position = Vector3.back * 10;

        _mainCamera = cameraObject.AddComponent<Camera>();
        _mainCamera.orthographic = true;
        _mainCamera.orthographicSize = 10;
    }

    private RacketBehavior CreateRacket()
    {
        GameObject racketObject = new GameObject("Racket");
        RacketBehavior racket = racketObject.AddComponent<RacketBehavior>();

        // This is just to have a visual on the racket!
        Texture2D tex = new Texture2D(16, 64);
        SpriteRenderer spr = racketObject.AddComponent<SpriteRenderer>();
        spr.sprite = Sprite.Create(
            tex,
            new Rect(0, 0, tex.width, tex.height),
            Vector2.zero);

        return racket;
    }

    [UnityTest]
    public IEnumerator PaddleMoveUpTest()
    {
        CreateCamera();
        RacketBehavior racket = CreateRacket();

        float startY = racket.transform.position.y;
        racket.SetDirection(1);
        yield return new WaitForSeconds(1f);

        Assert.IsTrue(racket.transform.position.y > startY);
    }

    [UnityTest]
    public IEnumerator PaddleMoveDownTest()
    {
        CreateCamera();
        RacketBehavior racket = CreateRacket();

        float startY = racket.transform.position.y;
        racket.SetDirection(-1);
        yield return new WaitForSeconds(1f);

        Assert.IsTrue(racket.transform.position.y < startY);
    }


    [UnityTest]
    public IEnumerator PaddleStayTest()
    {
        CreateCamera();
        RacketBehavior racket = CreateRacket();

        float startY = racket.transform.position.y;
        yield return new WaitForSeconds(1f);

        Assert.AreEqual(racket.transform.position.y, startY);
    }
}
