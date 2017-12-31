def updateCamera(camera):
	v = camera.GetPos()
	v2 = Vector3(v.x, v.y+1, v.z+1)
	camera.SetPos(v2)

camera = tycoon.GetCamera()
camera.SetUpdateFunction(updateCamera)