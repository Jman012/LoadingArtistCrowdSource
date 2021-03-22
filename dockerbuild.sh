LACSVersion=$(cat VERSION.txt)
docker build -t loadingartistcrowdsource:latest -t loadingartistcrowdsource:$LACSVersion -f LoadingArtistCrowdSource/Server/Dockerfile .
