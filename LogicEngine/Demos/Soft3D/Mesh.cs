//==============================================================================
// Copyright (C) 2015 Peng Guang Hui
// All rights reserved
//
// Create by 彭光辉 at 2015/10/16 20:30:00
// Email: gh.peng@qq.com
//==============================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using LogicEngine;

namespace Demos.Soft3D
{
    public class Mesh
    {
        public string Name { get; set; }
        public Vector3f[] Vertices { get; private set; }
        public Vector3f Position { get; set; }
        public Vector3f Rotation { get; set; }

        public Mesh(string name, int verticesCount)
        {
            Vertices = new Vector3f[verticesCount];
            Name = name;
        }
    }

    public enum PrimitiveType
    {
        Cube
    }

    public static class UtilPrimitive
    {
        public static Mesh Create(PrimitiveType type)
        {
            switch (type)
            {
                case PrimitiveType.Cube:
                    var mesh = new Mesh("Cube", 8);
                    mesh.Vertices[0] = new Vector3f(-1, 1, 1);
                    mesh.Vertices[1] = new Vector3f(1, 1, 1);
                    mesh.Vertices[2] = new Vector3f(-1, -1, 1);
                    mesh.Vertices[3] = new Vector3f(-1, -1, -1);
                    mesh.Vertices[4] = new Vector3f(-1, 1, -1);
                    mesh.Vertices[5] = new Vector3f(1, 1, -1);
                    mesh.Vertices[6] = new Vector3f(1, -1, 1);
                    mesh.Vertices[7] = new Vector3f(1, -1, -1);
                    return mesh;
            }
            return null;
        }
    }
}