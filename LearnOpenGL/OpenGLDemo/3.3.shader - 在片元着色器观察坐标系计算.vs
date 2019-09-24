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

	//�˴����˼�������ķ��߾���Ϊ�˷�ֹ��ģ�ͽ��зǵȱ����ŵ��·��ߴ���
	fragNormal = mat3(transpose(inverse(view * model))) * normal;
	lightPos = vec3(view * vec4(lightPostion,1.0));
}