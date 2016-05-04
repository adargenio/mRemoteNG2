node {
  git url: 'https://github.com/mRemoteNG/mRemoteNG.git'
  def msbuildFolder = tool 'MSBuild 14.0'
  bat "${msbuildFolder}\\msbuild.exe mRemoteV1.sln"
}