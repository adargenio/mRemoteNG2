node {
	def jobDir = pwd()
	def vsToolsDir = "C:\\Program Files (x86)\\Microsoft Visual Studio 14.0\\Common7\\Tools"
	def vsExtensionsDir = "C:\\Program Files (x86)\\Microsoft Visual Studio 14.0\\Common7\\IDE\\CommonExtensions\\Microsoft\\TestWindow"

	stage 'Build mRemoteNG'
	bat "\"${vsToolsDir}\\VsDevCmd.bat\" && msbuild.exe \"${jobDir}@script\\mRemoteV1.sln\""

	stage 'Run Unit Tests'
	bat "\"${vsToolsDir}\\VsDevCmd.bat\" && VSTest.Console.exe /TestAdapterPath:%LOCALAPPDATA%\Microsoft\VisualStudio\14.0\Extensions \"${jobDir}@script\\mRemoteNGTests\\bin\\debug\\mRemoteNGTests.dll\""
}