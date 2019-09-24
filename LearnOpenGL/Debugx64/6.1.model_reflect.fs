#version 330 core
out vec4 FragColor;

in vec2 TexCoords;
in vec3 fragNormal;
in vec3 fragPos;

uniform sampler2D texture_diffuse1;
uniform sampler2D texture_specular1;
uniform sampler2D texture_reflect1;

uniform samplerCube skybox;

uniform int ntype;

struct lightMaterial
{
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};

uniform lightMaterial light;
uniform vec3 viewPos;
uniform vec3 lightPos;

void main()
{    
    vec3 ambient = light.ambient * vec3(texture(texture_diffuse1, TexCoords));

    vec3 normal = normalize(fragNormal);

    vec3 lightDir = normalize(lightPos - fragPos);
    float diffuseAngel = max(dot(lightDir,normal),0.0);
    vec3 diffuse = light.diffuse * diffuseAngel * vec3(texture(texture_diffuse1, TexCoords));

    vec3 viewDir = normalize(viewPos - fragPos);
    vec3 reflectDir = reflect(-lightDir,normal);
    float specularAngel = pow(max(dot(reflectDir,viewDir),0.0),32);
    vec3 specular = light.specular * specularAngel * vec3(texture(texture_specular1, TexCoords));

    //反射强度
	vec3 reflectTen = vec3(texture(texture_reflect1,TexCoords));
    //反射向量
	vec3 reflectVec = normalize(reflect(-viewDir,normal));
    //反射颜色
	vec3 reflectColor = vec3(texture(skybox, reflectVec)) ;

    //空气和玻璃传播介质比值
    float ratio = 1.00 / 1.52;
    //折射向量
    vec3 refractVec = refract(fragPos-viewPos, normalize(normal), ratio);
    //折射颜色
    vec3 refractColor = vec3(texture(skybox, normalize(refractVec)));

    if(ntype == 0)
    { 
        //反射+Phong光照综合效果
        FragColor = vec4(ambient + diffuse + specular + reflectColor * reflectTen,1.0);
    }
    else if(ntype == 1)
    {
         //反射效果
	     FragColor = vec4(reflectColor,1.0);
    }
    else if(ntype == 2)
    {
        //折射效果
        FragColor = vec4(refractColor,1.0);
    }
}