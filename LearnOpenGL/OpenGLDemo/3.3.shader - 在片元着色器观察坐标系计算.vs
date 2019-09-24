#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 normal;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;
uniform vec3 lightPostion;

out vec3 fragNormal;
out vec3 fragPos;
out vec3 lightPos;

void main()
{
	gl_Position = projection * view * model * vec4(aPos, 1.0);
	fragPos = vec3(view * model * vec4(aPos, 1.0));

	//此处乘了计算出来的法线矩阵，为了防止对模型进行非等比缩放导致法线错误
	fragNormal = mat3(transpose(inverse(view * model))) * normal;
	lightPos = vec3(view * vec4(lightPostion,1.0));
}