node {
	def jobDir = pwd()
	def vsToolsDir = "C:\\Program Files (x86)\\Microsoft Visual Studio 14.0\\Common7\\Tools"
	def vsExtensionsDir = "C:\\Program Files (x86)\\Microsoft Visual Studio 14.0\\Common7\\IDE\\CommonExtensions\\Microsoft\\TestWindow"

	stage 'Build mRemoteNG'
	bat "\"${vsToolsDir}\\VsDevCmd.bat\" && msbuild.exe /nologo \"${jobDir}@script\\mRemoteV1.sln\" && exit"

	stage 'Run Unit Tests'
	def nunitTestAdapterPath = "C:\\Users\\Administrator\\AppData\\Local\\Microsoft\\VisualStudio\\14.0\\Extensions"
	bat "\"${vsToolsDir}\\VsDevCmd.bat\" && VSTest.Console.exe /TestAdapterPath:${nunitTestAdapterPath} \"${jobDir}@script\\mRemoteNGTests\\bin\\debug\\mRemoteNGTests.dll\""
}