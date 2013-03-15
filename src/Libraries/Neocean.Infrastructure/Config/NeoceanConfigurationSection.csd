<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="b9f93722-cefd-4a7d-be60-a2c498aee952" namespace="Neocean.Infrastructure.Config" xmlSchemaNamespace="urn:Neocean.Infrastructure.Config" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
  <typeDefinitions>
    <externalType name="String" namespace="System" />
    <externalType name="Boolean" namespace="System" />
    <externalType name="Int32" namespace="System" />
    <externalType name="Int64" namespace="System" />
    <externalType name="Single" namespace="System" />
    <externalType name="Double" namespace="System" />
    <externalType name="DateTime" namespace="System" />
    <externalType name="TimeSpan" namespace="System" />
  </typeDefinitions>
  <configurationElements>
    <configurationSection name="NeoceanConfigurationSection" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="neoceanConfigurationSection">
      <elementProperties>
        <elementProperty name="Memcache" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="memcache" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/b9f93722-cefd-4a7d-be60-a2c498aee952/MemcacheConfigurationElement" />
          </type>
        </elementProperty>
        <elementProperty name="EmailClient" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="emailClient" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/b9f93722-cefd-4a7d-be60-a2c498aee952/EmailClientConfigurationElement" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElement name="MemcacheConfigurationElement">
      <elementProperties>
        <elementProperty name="SocketPool" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="socketPool" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/b9f93722-cefd-4a7d-be60-a2c498aee952/SocketPoolConfigurationElement" />
          </type>
        </elementProperty>
        <elementProperty name="Servers" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="servers" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/b9f93722-cefd-4a7d-be60-a2c498aee952/ServersConfigurationElementCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationElement>
    <configurationElement name="SocketPoolConfigurationElement">
      <attributeProperties>
        <attributeProperty name="MinPoolSize" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="minPoolSize" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/b9f93722-cefd-4a7d-be60-a2c498aee952/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="MaxPoolSize" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="maxPoolSize" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/b9f93722-cefd-4a7d-be60-a2c498aee952/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="ConnectionTimeout" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="connectionTimeout" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/b9f93722-cefd-4a7d-be60-a2c498aee952/TimeSpan" />
          </type>
        </attributeProperty>
        <attributeProperty name="DeadTimeout" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="deadTimeout" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/b9f93722-cefd-4a7d-be60-a2c498aee952/TimeSpan" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElement name="ServerAddConfigurationElement">
      <attributeProperties>
        <attributeProperty name="Address" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="address" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/b9f93722-cefd-4a7d-be60-a2c498aee952/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Port" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="port" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/b9f93722-cefd-4a7d-be60-a2c498aee952/Int64" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElementCollection name="ServersConfigurationElementCollection" xmlItemName="serverAddConfigurationElement" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/b9f93722-cefd-4a7d-be60-a2c498aee952/ServerAddConfigurationElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="EmailClientConfigurationElement">
      <attributeProperties>
        <attributeProperty name="Host" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="host" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/b9f93722-cefd-4a7d-be60-a2c498aee952/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Port" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="port" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/b9f93722-cefd-4a7d-be60-a2c498aee952/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="UserName" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="userName" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/b9f93722-cefd-4a7d-be60-a2c498aee952/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Password" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="password" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/b9f93722-cefd-4a7d-be60-a2c498aee952/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="EnableSsl" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="enableSsl" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/b9f93722-cefd-4a7d-be60-a2c498aee952/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="Sender" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="sender" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/b9f93722-cefd-4a7d-be60-a2c498aee952/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
</configurationSectionModel>