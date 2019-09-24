#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 normal;
layout (location = 2) in vec2 textc;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

out vec3 fragNormal;
out vec3 fragPos;
out vec2 TexCoords;

void main()
{
	gl_Position = projection * view * model * vec4(aPos, 1.0);
	fragPos = vec3(model * vec4(aPos, 1.0));
	TexCoords = textc;

	//此处乘了计算出来的法线矩阵，为了防止对模型进行非等比缩放导致法线错误
	fragNormal = mat3(transpose(inverse(model))) * normal;
}