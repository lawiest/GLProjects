#version 330 core

out vec4 FragColor;

uniform vec3 objectColor;
uniform vec3 lightColor;

in vec3 fragNormal;
in vec3 fragPos;
in vec3 lightPos;

void main()
{ 
	//���㻷����Թ�ԴӰ��ֻ�Ǹ�������ϵ��
	float ambientStrength = 0.3;
	vec3 resultAmbient = ambientStrength * lightColor;

	//����������Թ�ԴӰ��
	vec3 normal = normalize(fragNormal);
	vec3 lightVec = normalize(lightPos - fragPos);
	float angle = max(dot(normal,lightVec),0);
	//�Ƕȴ���н�����ֵ��ԽСϵ��Խ�Ӵ�Խ�ӽ���1��Խ��
	vec3 resultDiffuse = angle * lightColor;

	//���㾵������
	float specularStrength = 1.0;
	vec3 lookVec = normalize(-fragPos);
	vec3 reflectVec = reflect(-lightVec,normal);
	float spec = pow(max(dot(lookVec,reflectVec),0),32);
	vec3 specularResult = specularStrength * spec * lightColor;

	//����������Ӧ�õ�������
	vec3 result = (resultAmbient + resultDiffuse + specularResult) * objectColor;

    FragColor = vec4(result, 1.0);
}