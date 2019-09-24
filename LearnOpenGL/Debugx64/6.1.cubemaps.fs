#version 330 core
out vec4 FragColor;

in vec3 fagPositon;
in vec3 fagNormal;

uniform samplerCube skybox;
uniform vec3 cameraPos;

void main()
{    
	vec3 normal = normalize(fagNormal);

	vec3 reflectVec = normalize(reflect((fagPositon - cameraPos),normal));

	FragColor = texture(skybox, reflectVec);
}