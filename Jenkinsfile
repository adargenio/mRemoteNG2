node {
	def jobDir = pwd()
	def msbuildFolder = tool 'MSBuild 14.0'
	bat "${msbuildFolder}\\msbuild.exe \"${jobDir}@script\\mRemoteV1.sln\""
}