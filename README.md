# Clean Architecture MS2 Template

In this repository I have prepared a **clean architectural Template** for **visual studio** so you can start your project very quickly with clean architecture!

![image](https://user-images.githubusercontent.com/39134345/131919207-dc713e42-c128-4d9d-9ac4-8cf259bd531e.png)


# Get Started!

#### 1. Download zip file of lastest release : [link](https://github.com/MSaniee/Clean-Architecture-MS2-Template/releases/download/v0.0.1/Clean.Architecture.MS2.Template.zip)

#### 2. Go to Documents\Visual Studio Version\Templates\ProjectTemplates\Visual C#  and paste zip file :

![image](https://user-images.githubusercontent.com/39134345/131916769-ea1fd24d-c69b-4bab-ba16-6c35b39e8688.png)


#### 3. Go to visual studio -> create new project dialog You will see the newly created template as shown below.

![image](https://user-images.githubusercontent.com/39134345/131917467-d8c6311b-d1e6-43f8-859e-7c0945810de0.png)


#### 4. Create new project :

![image](https://user-images.githubusercontent.com/39134345/131917685-3a226c8b-6f03-4e4e-a96f-d4f5b81a08bc.png)





# Review of architecture and layering

![Clean Architecture MS2](https://user-images.githubusercontent.com/39134345/131918266-f90f9b9c-0195-4067-9729-28920be57dd8.png)


#### First Layer : Common

In this layer there are tools and classes that perhaps they be used in All layers . Actually this layer has nothing to do with project subject and utilities of this layer help us to Improve codding and prevents duplication of classes or methods.
In this layer we have :

PersianDate.cs 
Resources : *.resx
SeparationMoney.cs
StringExtensions.cs
SecutrityHelper.cs

Also, according to the definition of this layer, shared libraries are located in this area. you can more study about shared libraries in this [link](https://dev.to/rionmonster/sharing-is-caring-using-shared-projects-in-aspnet-e17)




