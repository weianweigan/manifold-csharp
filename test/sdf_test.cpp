// Copyright 2022 The Manifold Authors.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#include "manifold.h"
#include "test.h"

using namespace manifold;

struct CubeVoid {
  float operator()(glm::vec3 p) const {
    const glm::vec3 min = p + glm::vec3(1);
    const glm::vec3 max = glm::vec3(1) - p;
    const float min3 = glm::min(min.x, glm::min(min.y, min.z));
    const float max3 = glm::min(max.x, glm::min(max.y, max.z));
    return -1.0f * glm::min(min3, max3);
  }
};

struct Layers {
  float operator()(glm::vec3 p) const {
    int a = glm::mod(glm::round(2 * p.z), 4.0f);
    return a == 0 ? 1 : (a == 2 ? -1 : 0);
  }
};

TEST(SDF, CubeVoid) {
  CubeVoid voidSDF;

  EXPECT_EQ(voidSDF({0, 0, 0}), -1);
  EXPECT_EQ(voidSDF({0, 0, 1}), 0);
  EXPECT_EQ(voidSDF({0, 1, 1}), 0);
  EXPECT_EQ(voidSDF({-1, 0, 0}), 0);
  EXPECT_EQ(voidSDF({1, 1, -1}), 0);
  EXPECT_EQ(voidSDF({2, 0, 0}), 1);
  EXPECT_EQ(voidSDF({2, -2, 0}), 1);
  EXPECT_EQ(voidSDF({-2, 2, 2}), 1);
}

TEST(SDF, Bounds) {
  const float size = 4;
  const float edgeLength = 1;

  MeshGL levelSet = MeshGL::LevelSet(
      CubeVoid(), {glm::vec3(-size / 2), glm::vec3(size / 2)}, edgeLength);
  Manifold cubeVoid(levelSet);
  Box bounds = cubeVoid.BoundingBox();
  const float precision = cubeVoid.Precision();
#ifdef MANIFOLD_EXPORT
  if (options.exportModels) ExportMesh("cubeVoid.glb", levelSet, {});
#endif

  EXPECT_EQ(cubeVoid.Status(), Manifold::Error::NoError);
  EXPECT_EQ(cubeVoid.Genus(), -1);
  const float outerBound = size / 2 + edgeLength / 2;
  EXPECT_NEAR(bounds.min.x, -outerBound, precision);
  EXPECT_NEAR(bounds.min.y, -outerBound, precision);
  EXPECT_NEAR(bounds.min.z, -outerBound, precision);
  EXPECT_NEAR(bounds.max.x, outerBound, precision);
  EXPECT_NEAR(bounds.max.y, outerBound, precision);
  EXPECT_NEAR(bounds.max.z, outerBound, precision);
}

TEST(SDF, Bounds2) {
  const float size = 4;
  const float edgeLength = 1;

  Manifold cubeVoid(MeshGL::LevelSet(
      CubeVoid(), {glm::vec3(-size / 2), glm::vec3(size / 2)}, edgeLength));
  Box bounds = cubeVoid.BoundingBox();
  const float precision = cubeVoid.Precision();
#ifdef MANIFOLD_EXPORT
  if (options.exportModels)
    ExportMesh("cubeVoid2.glb", cubeVoid.GetMeshGL(), {});
#endif

  EXPECT_EQ(cubeVoid.Status(), Manifold::Error::NoError);
  EXPECT_EQ(cubeVoid.Genus(), -1);
  const float outerBound = size / 2 + edgeLength / 2;
  EXPECT_NEAR(bounds.min.x, -outerBound, precision);
  EXPECT_NEAR(bounds.min.y, -outerBound, precision);
  EXPECT_NEAR(bounds.min.z, -outerBound, precision);
  EXPECT_NEAR(bounds.max.x, outerBound, precision);
  EXPECT_NEAR(bounds.max.y, outerBound, precision);
  EXPECT_NEAR(bounds.max.z, outerBound, precision);
}

TEST(SDF, Surface) {
  const float size = 4;
  const float edgeLength = 0.5;

  Manifold cubeVoid(MeshGL::LevelSet(
      CubeVoid(), {glm::vec3(-size / 2), glm::vec3(size / 2)}, edgeLength));

  Manifold cube = Manifold::Cube(glm::vec3(size), true);
  cube -= cubeVoid;
  Box bounds = cube.BoundingBox();
  const float precision = cube.Precision();
#ifdef MANIFOLD_EXPORT
  if (options.exportModels) ExportMesh("cube.gltf", cube.GetMesh(), {});
#endif

  EXPECT_EQ(cubeVoid.Status(), Manifold::Error::NoError);
  EXPECT_EQ(cube.Genus(), 0);
  auto prop = cube.GetProperties();
  EXPECT_NEAR(prop.volume, 8, 0.001);
  EXPECT_NEAR(prop.surfaceArea, 24, 0.001);
  EXPECT_NEAR(bounds.min.x, -1, precision);
  EXPECT_NEAR(bounds.min.y, -1, precision);
  EXPECT_NEAR(bounds.min.z, -1, precision);
  EXPECT_NEAR(bounds.max.x, 1, precision);
  EXPECT_NEAR(bounds.max.y, 1, precision);
  EXPECT_NEAR(bounds.max.z, 1, precision);
}

TEST(SDF, Resize) {
  const float size = 20;
  Manifold layers(
      MeshGL::LevelSet(Layers(), {glm::vec3(0), glm::vec3(size)}, 1));
#ifdef MANIFOLD_EXPORT
  if (options.exportModels) ExportMesh("layers.gltf", layers.GetMesh(), {});
#endif

  EXPECT_EQ(layers.Status(), Manifold::Error::NoError);
  EXPECT_EQ(layers.Genus(), -8);
}

TEST(SDF, SineSurface) {
  MeshGL surface = MeshGL::LevelSet(
      [](glm::vec3 p) {
        float mid = glm::sin(p.x) + glm::sin(p.y);
        return (p.z > mid - 0.5 && p.z < mid + 0.5) ? 1.0f : -1.0f;
      },
      {glm::vec3(-1.75 * glm::pi<float>()), glm::vec3(1.75 * glm::pi<float>())},
      1);
  Manifold smoothed = Manifold(surface).SmoothOut(180).RefineToLength(0.05);

  EXPECT_EQ(smoothed.Status(), Manifold::Error::NoError);
  EXPECT_EQ(smoothed.Genus(), 38);

#ifdef MANIFOLD_EXPORT
  if (options.exportModels)
    ExportMesh("sinesurface.glb", smoothed.GetMeshGL(), {});
#endif
}

TEST(SDF, Blobs) {
  const float blend = 1;
  std::vector<glm::vec4> balls = {{0, 0, 0, 2},     //
                                  {1, 2, 3, 2},     //
                                  {-2, 2, -2, 1},   //
                                  {-2, -3, -2, 2},  //
                                  {-3, -1, -3, 1},  //
                                  {2, -3, -2, 2},   //
                                  {-2, 3, 2, 2},    //
                                  {-2, -3, 2, 2},   //
                                  {1, -1, 1, -2},   //
                                  {-4, -3, -2, 1}};
  MeshGL blobs = MeshGL::LevelSet(
      [&balls, blend](glm::vec3 p) {
        float d = 0;
        for (const auto& ball : balls) {
          d += glm::sign(ball.w) *
               glm::smoothstep(
                   -blend, blend,
                   glm::abs(ball.w) - glm::length(glm::vec3(ball) - p));
        }
        return d;
      },
      {glm::vec3(-5), glm::vec3(5)}, 0.02, 0.5);

  const int chi = blobs.NumVert() - blobs.NumTri() / 2;
  const int genus = 1 - chi / 2;
  EXPECT_EQ(genus, 0);

#ifdef MANIFOLD_EXPORT
  if (options.exportModels) ExportMesh("blobs.glb", blobs, {});
#endif
}