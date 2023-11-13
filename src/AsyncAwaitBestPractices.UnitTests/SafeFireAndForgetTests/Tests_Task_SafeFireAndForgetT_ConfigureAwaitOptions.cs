#if NET8_0_OR_GREATER
using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AsyncAwaitBestPractices.UnitTests;

class Tests_Task_SafeFIreAndForgetT_ConfigureAwaitOptions : BaseTest
{
	[SetUp]
	public void BeforeEachTest()
	{
		SafeFireAndForgetExtensions.Initialize(false);
		SafeFireAndForgetExtensions.RemoveDefaultExceptionHandling();
	}

	[TearDown]
	public void AfterEachTest()
	{
		SafeFireAndForgetExtensions.Initialize(false);
		SafeFireAndForgetExtensions.RemoveDefaultExceptionHandling();
	}

	[Test]
	public async Task SafeFireAndForget_HandledException()
	{
		//Arrange
		NullReferenceException? exception = null;

		//Act
		NoParameterDelayedNullReferenceExceptionTask().SafeFireAndForget<NullReferenceException>(ConfigureAwaitOptions.None, ex => exception = ex);
		await NoParameterTask();
		await NoParameterTask();

		//Assert
		Assert.IsNotNull(exception);
	}

	[Test]
	public async Task SafeFireAndForget_HandledException_ConfigureAwaitOptionsSuppressThrowing()
	{
		//Arrange
		NullReferenceException? exception = null;

		//Act
		NoParameterDelayedNullReferenceExceptionTask().SafeFireAndForget<NullReferenceException>(ConfigureAwaitOptions.SuppressThrowing, ex => exception = ex);
		await NoParameterTask();
		await NoParameterTask();

		//Assert
		Assert.IsNull(exception);
	}

	[Test]
	public async Task SafeFireAndForgetT_SetDefaultExceptionHandling_NoParams()
	{
		//Arrange
		Exception? exception = null;
		SafeFireAndForgetExtensions.SetDefaultExceptionHandling(ex => exception = ex);

		//Act
		NoParameterDelayedNullReferenceExceptionTask().SafeFireAndForget(ConfigureAwaitOptions.None);
		await NoParameterTask();
		await NoParameterTask();

		//Assert
		Assert.IsNotNull(exception);
	}
	
	[Test]
	public async Task SafeFireAndForgetT_SetDefaultExceptionHandling_ConfigureAwaitOptionsSuppressThrowing()
	{
		//Arrange
		Exception? exception = null;
		SafeFireAndForgetExtensions.SetDefaultExceptionHandling(ex => exception = ex);

		//Act
		NoParameterDelayedNullReferenceExceptionTask().SafeFireAndForget(ConfigureAwaitOptions.SuppressThrowing);
		await NoParameterTask();
		await NoParameterTask();

		//Assert
		Assert.IsNull(exception);
	}

	[Test]
	public async Task SafeFireAndForgetT_SetDefaultExceptionHandling_WithParams()
	{
		//Arrange
		Exception? exception1 = null;
		NullReferenceException? exception2 = null;
		SafeFireAndForgetExtensions.SetDefaultExceptionHandling(ex => exception1 = ex);

		//Act
		NoParameterDelayedNullReferenceExceptionTask().SafeFireAndForget<NullReferenceException>(ConfigureAwaitOptions.None, ex => exception2 = ex);
		await NoParameterTask();
		await NoParameterTask();

		//Assert
		Assert.IsNotNull(exception1);
		Assert.IsNotNull(exception2);
	}
	
	[Test]
	public async Task SafeFireAndForgetT_SetDefaultExceptionHandling_WithParams_ConfigureAwaitOptionsSuppressThrowing()
	{
		//Arrange
		Exception? exception1 = null;
		NullReferenceException? exception2 = null;
		SafeFireAndForgetExtensions.SetDefaultExceptionHandling(ex => exception1 = ex);

		//Act
		NoParameterDelayedNullReferenceExceptionTask().SafeFireAndForget<NullReferenceException>(ConfigureAwaitOptions.SuppressThrowing, ex => exception2 = ex);
		await NoParameterTask();
		await NoParameterTask();

		//Assert
		Assert.IsNull(exception1);
		Assert.IsNull(exception2);
	}
}
#endif