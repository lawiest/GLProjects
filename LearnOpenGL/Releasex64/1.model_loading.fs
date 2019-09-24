#version 330 core
out vec4 FragColor;

in vec2 TexCoords;
in vec3 fragNormal;
in vec3 fragPos;

uniform sampler2D texture_diffuse1;
uniform sampler2D texture_specular1;

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

    FragColor = vec4(ambient + diffuse + specular,1.0);
}