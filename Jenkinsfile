node {
	def jobDir = System.getProperty("user.dir")
	dir '${jobDir}@script'
	def msbuildFolder = tool 'MSBuild 14.0'
	bat "${msbuildFolder}\\msbuild.exe mRemoteV1.sln"
}