﻿/*
This file was generated by template ../NDArray.Elementwise.tt
In case you want to do some changes do the following 

1 ) adapt the tt file
2 ) execute powershell file "GenerateCode.ps1" on root level

*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Numerics;
using NumSharp.Shared;

namespace NumSharp.Backends
{
    public abstract partial class DefaultEngine
    {
        public virtual NDArray Sum(NDArray x, int axis = -1)
        {
            switch (axis)
            {
                case -1:
                    return Sum(x);
                case 0:
                    return Sum0(x, axis);
                case 1:
                    return Sum1(x, axis);
                case 2:
                    return Sum2(x, axis);
                default:
                    throw new NotImplementedException($"DefaultEngine sum {x.dtype.Name} axis: {axis}");
            }
        }

        private NDArray Sum(NDArray x)
        {
            switch (Type.GetTypeCode(x.dtype))
            {
                case TypeCode.Int32:
                    return x.Data<int>().Sum();
                case TypeCode.Single:
                    return x.Data<float>().Sum();
            }

            throw new NotImplementedException($"DefaultEngine sum {x.dtype.Name}");
        }

        private NDArray Sum0(NDArray x, int axis)
        {
            var shape = Shape.GetShape(x.shape, axis);
            var size = Shape.GetSize(shape);
            int i = 0;

            switch (Type.GetTypeCode(x.dtype))
            {
                case TypeCode.Int32:
                    {
                        // allocate continuous memory
                        var data = new int[size];
                        switch (shape.Length)
                        {
                            case 1:
                                for (int d0 = 0; d0 < shape[0]; d0++)
                                {
                                    for (int d1 = 0; d1 < x.shape[axis]; d1++)
                                        data[i] += x.GetInt32(d1, d0);
                                    i++;
                                }
                                break;

                            case 2:
                                for (int d0 = 0; d0 < shape[0]; d0++)
                                {
                                    for (int d1 = 0; d1 < shape[1]; d1++)
                                    {
                                        for (int d2 = 0; d2 < x.shape[axis]; d2++)
                                            data[i] += x.GetInt32(d2, d0, d1);
                                        i++;
                                    }
                                }
                                break;
                                
                        }

                        return new NDArray(data, shape);
                    }

                case TypeCode.Single:
                    {
                        // allocate continuous memory
                        var data = new float[size];
                        switch (shape.Length)
                        {
                            case 1:
                                for (int d0 = 0; d0 < shape[0]; d0++)
                                {
                                    for (int d1 = 0; d1 < x.shape[axis]; d1++)
                                        data[i] += x.GetSingle(d1, d0);
                                    i++;
                                }

                                return new NDArray(data, shape);

                            case 2:
                                for (int d0 = 0; d0 < shape[0]; d0++)
                                {
                                    for (int d1 = 0; d1 < shape[1]; d1++)
                                    {
                                        for (int d2 = 0; d2 < x.shape[axis]; d2++)
                                            data[i] += x.GetInt32(d2, d0, d1);
                                        i++;
                                    }
                                }

                                return new NDArray(data, shape);
                        }
                    }
                    break;
            }

            throw new NotImplementedException($"DefaultEngine sum {x.dtype.Name} axis: {axis}");
        }

        private NDArray Sum1(NDArray x, int axis)
        {
            var shape = Shape.GetShape(x.shape, axis);
            var size = Shape.GetSize(shape);
            int i = 0;

            switch (Type.GetTypeCode(x.dtype))
            {
                case TypeCode.Int32:
                    {
                        // allocate continuous memory
                        var data = new int[size];
                        switch (shape.Length)
                        {
                            case 1:
                                for (int d0 = 0; d0 < shape[0]; d0++)
                                {
                                    for (int d1 = 0; d1 < x.shape[axis]; d1++)
                                        data[i] += x.GetInt32(d0, d1);
                                    i++;
                                }
                                break;

                            case 2:
                                for (int d0 = 0; d0 < shape[0]; d0++)
                                {
                                    for (int d1 = 0; d1 < shape[1]; d1++)
                                    {
                                        for (int d2 = 0; d2 < x.shape[axis]; d2++)
                                            data[i] += x.GetInt32(d0, d2, d1);
                                        i++;
                                    }
                                }
                                break;
                        }

                        return new NDArray(data, shape);
                    }

                case TypeCode.Single:
                    {
                        // allocate continuous memory
                        var data = new float[size];
                        switch (shape.Length)
                        {
                            case 1:
                                for (int d0 = 0; d0 < shape[0]; d0++)
                                {
                                    for (int d1 = 0; d1 < x.shape[axis]; d1++)
                                        data[i] += x.GetSingle(d0, d1);
                                    i++;
                                }
                                break;

                            case 2:
                                for (int d0 = 0; d0 < shape[0]; d0++)
                                {
                                    for (int d1 = 0; d1 < shape[1]; d1++)
                                    {
                                        for (int d2 = 0; d2 < x.shape[axis]; d2++)
                                            data[i] += x.GetSingle(d0, d2, d1);
                                        i++;
                                    }
                                }
                                break;
                        }

                        return new NDArray(data, shape);
                    }
            }

            throw new NotImplementedException($"DefaultEngine sum {x.dtype.Name} axis: {axis}");
        }

        private NDArray Sum2(NDArray x, int axis)
        {
            var shape = Shape.GetShape(x.shape, axis);
            var size = Shape.GetSize(shape);
            int i = 0;

            switch (Type.GetTypeCode(x.dtype))
            {
                case TypeCode.Int32:
                    {
                        // allocate continuous memory
                        var data = new int[size];
                        switch (shape.Length)
                        {
                            case 2:
                                for (int d0 = 0; d0 < shape[0]; d0++)
                                {
                                    for (int d1 = 0; d1 < shape[1]; d1++)
                                    {
                                        for (int d2 = 0; d2 < x.shape[axis]; d2++)
                                            data[i] += x.GetInt32(d0, d1, d2);
                                        i++;
                                    }
                                }
                                break;
                        }

                        return new NDArray(data, shape);
                    }

                case TypeCode.Single:
                    {
                        // allocate continuous memory
                        var data = new float[size];
                        switch (shape.Length)
                        {
                            case 2:
                                for (int d0 = 0; d0 < shape[0]; d0++)
                                {
                                    for (int d1 = 0; d1 < shape[1]; d1++)
                                    {
                                        for (int d2 = 0; d2 < x.shape[axis]; d2++)
                                            data[i] += x.GetSingle(d0, d1, d2);
                                        i++;
                                    }
                                }
                                break;
                        }

                        return new NDArray(data, shape);
                    }
            }

            throw new NotImplementedException($"DefaultEngine sum {x.dtype.Name} axis: {axis}");
        }
    }

}

