node {
	def jobDir = pwd()
	def vsDevCmdBat = 'C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\Tools\VsDevCmd.bat'
	bat "\"${vsDevCmdBat}\" && msbuild.exe \"${jobDir}@script\\mRemoteV1.sln\""
}