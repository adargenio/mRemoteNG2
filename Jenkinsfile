node {
	def jobDir = pwd()
	def vsToolsDir = "C:\\Program Files (x86)\\Microsoft Visual Studio 14.0\\Common7\\Tools"
	bat "\"${vsToolsDir}\\VsDevCmd.bat\" && msbuild.exe \"${jobDir}@script\\mRemoteV1.sln\""
}