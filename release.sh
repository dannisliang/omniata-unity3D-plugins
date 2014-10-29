#!/bin/sh

if [ ! -n "$1" ]; then
    echo "Give the version as a parameter"
    exit -1
fi

VERSION=$1

UNITY_PACKAGE=OmniataSDK-${VERSION}.unitypackage
if [ ! -f "$UNITY_PACKAGE" ]; then
    echo "Unity package missing: $UNITY_PACKAGE. Build it first."
    exit -1
fi

PAGES_REPOSITY_DIR=../Omniata.github.io
if [ ! -d "$PAGES_REPOSITY_DIR" ]; then
    echo "No Github pages clone in $PAGES_REPOSITY_DIR. Clone that first with 'git clone git@github.com:Omniata/Omniata.github.io.git'"
    exit -1
fi

# Update version
# perl -pi -e "s/#VERSION#/${VERSION}/g" Assets/Omniata/Omniata.cs

git tag -a v$VERSION -m "v${VERSION}"
git push -u origin master

# Clean and run doxygen
echo "Creating APIdoc"
rm -rf html
rm -rf latex
doxygen doxygen.config

# Deploy docs and binary
echo "Copying to Omniata repository"
DIR=`pwd`

PAGES_DIR_RELATIVE=docs/sdks/unity3d-plugin/$VERSION
PAGES_DIR=../Omniata.github.io/$PAGES_DIR_RELATIVE

rm -rf $PAGES_DIR
mkdir $PAGES_DIR
mkdir $PAGES_DIR/apidoc
cp -r html/* $PAGES_DIR/apidoc/
cp $UNITY_PACKAGE $PAGES_DIR/

echo "Commiting and pushing"
cd $PAGES_REPOSITY_DIR
git pull
git add $PAGES_DIR_RELATIVE
git commit -m "Unity3d plugin SDK ${VERSION}" $PAGES_DIR_RELATIVE
git push -u origin master

echo "Ready version $VERSION"

