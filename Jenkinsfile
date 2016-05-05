node {
	def jobDir = getClass().protectionDomain.codeSource.location.path
	echo "jobDir: ${jobDir}"
	dir "${jobDir}@script"
	def msbuildFolder = tool 'MSBuild 14.0'
	bat "${msbuildFolder}\\msbuild.exe mRemoteV1.sln"
}