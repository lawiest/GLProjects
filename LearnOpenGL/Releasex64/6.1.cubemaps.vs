#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;

out vec3 fagPositon;
out vec3 fagNormal;

layout (std140) uniform Matrice
{
	mat4 view;
	mat4 projection;
};

uniform mat4 model;

void main()
{
	fagNormal = vec3(transpose(inverse(model)) * vec4(aNormal, 1.0));
	fagPositon =   vec3(model * vec4(aPos, 1.0));
    gl_Position = projection * view * model * vec4(aPos, 1.0);

	gl_PointSize = gl_Position.z;
}