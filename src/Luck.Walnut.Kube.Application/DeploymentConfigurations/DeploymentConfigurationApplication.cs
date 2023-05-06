using k8s.Models;
using Luck.Framework.Exceptions;
using Luck.Framework.Extensions;
using Luck.Framework.UnitOfWorks;
using Luck.Walnut.Kube.Domain.AggregateRoots.DeploymentConfigurations;
using Luck.Walnut.Kube.Domain.Repositories;
using Luck.Walnut.Kube.Dto.DeploymentConfigurations;

namespace Luck.Walnut.Kube.Application.DeploymentConfigurations;

public class DeploymentConfigurationApplication : IDeploymentConfigurationApplication
{
    private readonly IDeploymentConfigurationRepository _deploymentConfigurationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeploymentConfigurationApplication(IDeploymentConfigurationRepository deploymentConfigurationRepository, IUnitOfWork unitOfWork)
    {
        _deploymentConfigurationRepository = deploymentConfigurationRepository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// 添加部署
    /// </summary>
    /// <param name="input"></param>
    /// <exception cref="BusinessException"></exception>
    public async Task CreateDeploymentConfigurationAsync(DeploymentInputDto input)
    {
        var deploymentConfiguration = input.DeploymentConfiguration;
        var masterContainerConfiguration = input.MasterContainerConfiguration;

        if (await CheckIsExitDeploymentConfigurationAsync(deploymentConfiguration.AppId, deploymentConfiguration.Name))
        {
            throw new BusinessException($"[{deploymentConfiguration.Name}]已存在，请刷新页面");
        }

        var applicationDeployment = new DeploymentConfiguration(deploymentConfiguration.EnvironmentName,
            deploymentConfiguration.ApplicationRuntimeType, deploymentConfiguration.DeploymentType, deploymentConfiguration.ChineseName, deploymentConfiguration.Name, deploymentConfiguration.AppId,
            deploymentConfiguration.NameSpaceId, deploymentConfiguration.Replicas, deploymentConfiguration.ImagePullSecretId,deploymentConfiguration.ClusterId);

        applicationDeployment.SetInitContainers(deploymentConfiguration.SideCarPlugins);
        applicationDeployment.SetStrategy(deploymentConfiguration.Strategy);
        applicationDeployment.AddMasterContainerConfiguration(masterContainerConfiguration);
        _deploymentConfigurationRepository.Add(applicationDeployment);
        await _unitOfWork.CommitAsync();
    }

    /// <summary>
    /// 修改部署
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    public async Task UpdateDeploymentConfigurationAsync(string id, DeploymentConfigurationInputDto input)
    {
        var applicationDeployment = await GetAndCheckDeploymentConfigurationAsync(id);
        applicationDeployment.SetDeploymentInfo(input);
        applicationDeployment.SetStrategy(input.Strategy);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateDeploymentConfigurationAsync(string id, string masterContainerId, DeploymentInputDto input)
    {
        var deploymentConfiguration = input.DeploymentConfiguration;
        var masterContainerConfiguration = input.MasterContainerConfiguration;
        var applicationDeployment = await GetAndCheckDeploymentConfigurationAsync(id);
        applicationDeployment.UpdateMasterContainerConfiguration(masterContainerId, masterContainerConfiguration);
        applicationDeployment.SetDeploymentInfo(deploymentConfiguration);
        applicationDeployment.SetStrategy(deploymentConfiguration.Strategy);
        await _unitOfWork.CommitAsync();
    }

    /// <summary>
    /// 删除部署
    /// </summary>
    /// <param name="id"></param>
    public async Task DeleteDeploymentConfigurationAsync(string id)
    {
        var applicationDeployment = await GetAndCheckDeploymentConfigurationAsync(id);
        _deploymentConfigurationRepository.Remove(applicationDeployment);
        await _unitOfWork.CommitAsync();
    }


    public async Task<DeploymentConfiguration> GetAndCheckDeploymentConfigurationAsync(string id)
    {
        var cluster = await _deploymentConfigurationRepository.FindDeploymentConfigurationByIdAsync(id);
        return cluster is null ? throw new BusinessException("部署不存在，请刷新页面") : cluster;
    }

    #region ApplicationContainer

    /// <summary>
    /// 添加容器配置
    /// </summary>
    /// <param name="deploymentConfigurationId"></param>
    /// <param name="input"></param>
    public async Task CreateDeploymentContainerConfigurationAsync(string deploymentConfigurationId, MasterContainerConfigurationInputDto input)
    {
        var applicationDeployment = await GetAndCheckDeploymentConfigurationAsync(deploymentConfigurationId);

        applicationDeployment.AddMasterContainerConfiguration(input);
        await _unitOfWork.CommitAsync();
    }

    /// <summary>
    /// 删除容器配置
    /// </summary>
    /// <param name="deploymentConfigurationId"></param>
    /// <param name="deploymentContainerConfigurationId"></param>
    /// <param name="input"></param>
    public async Task UpdateDeploymentContainerConfigurationAsync(string deploymentConfigurationId, string deploymentContainerConfigurationId, MasterContainerConfigurationInputDto input)
    {
        var applicationDeployment = await GetAndCheckDeploymentConfigurationAsync(deploymentConfigurationId);
        applicationDeployment.UpdateMasterContainerConfiguration(deploymentContainerConfigurationId, input);
        await _unitOfWork.CommitAsync();
    }

    /// <summary>
    /// 删除容器配置
    /// </summary>
    /// <param name="deploymentConfigurationId"></param>
    /// <param name="deploymentContainerConfigurationId"></param>
    public async Task DeleteDeploymentContainerConfigurationAsync(string deploymentConfigurationId, string deploymentContainerConfigurationId)
    {
        var applicationDeployment = await GetAndCheckDeploymentConfigurationAsync(deploymentConfigurationId);
        applicationDeployment.RemoveDeploymentContainerConfiguration(deploymentContainerConfigurationId);
        await _unitOfWork.CommitAsync();
    }

    #endregion


    #region 私有Check方法



    private async Task<bool> CheckIsExitDeploymentConfigurationAsync(string appId, string name)
    {
        var cluster = await _deploymentConfigurationRepository.FindDeploymentConfigurationByAppIdAndNameAsync(appId, name);
        return cluster is not null;
    }

    #endregion
}