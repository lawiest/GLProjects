#version 330 core

out vec4 FragColor;

uniform vec3 objectColor;
uniform vec3 lightColor;

in vec3 fragNormal;
in vec3 fragPos;
in vec3 lightPos;

void main()
{ 
	//计算环境光对光源影像，只是给个单纯系数
	float ambientStrength = 0.3;
	vec3 resultAmbient = ambientStrength * lightColor;

	//计算漫反射对光源影像
	vec3 normal = normalize(fragNormal);
	vec3 lightVec = normalize(lightPos - fragPos);
	float angle = max(dot(normal,lightVec),0);
	//角度代表夹角余弦值，越小系数越接大，越接近于1就越亮
	vec3 resultDiffuse = angle * lightColor;

	//计算镜面光分量
	float specularStrength = 1.0;
	vec3 lookVec = normalize(-fragPos);
	vec3 reflectVec = reflect(-lightVec,normal);
	float spec = pow(max(dot(lookVec,reflectVec),0),32);
	vec3 specularResult = specularStrength * spec * lightColor;

	//将各个因素应用到物体上
	vec3 result = (resultAmbient + resultDiffuse + specularResult) * objectColor;

    FragColor = vec4(result, 1.0);
}