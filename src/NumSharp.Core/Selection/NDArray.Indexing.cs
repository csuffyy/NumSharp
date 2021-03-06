﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using NumSharp.Generic;

namespace NumSharp
{
    public partial class NDArray
    {
        /// <summary>
        /// Get and set element wise data
        /// Low performance
        /// Use generic Data<T> and SetData<T>(value, shape) method for better performance
        /// </summary>
        /// <param name="select"></param>
        /// <returns></returns>
        public NDArray this[params int[] select]
        {
            get
            {
                return Storage.GetData(select);
            }
            
            set
            {
                Storage.SetData(value, select);
            }
        }
        
        public NDArray this[NDArray indexes]
        {
            get
            {
                NDArray selectedValues = null;

                switch (ndim)
                {
                    case 1:
                        if (dtype.Name == "Byte")
                            selectedValues = setValue1D<byte>(indexes);
                        if (dtype.Name == "Int32")
                            selectedValues = setValue1D<int>(indexes);
                        if (dtype.Name == "Single")
                            selectedValues = setValue1D<float>(indexes);
                        else if (dtype.Name == "Double")
                            selectedValues = setValue1D<double>(indexes);
                        break;

                    case 2:
                        if (dtype.Name == "Byte")
                            selectedValues = setValue2D<byte>(indexes);
                        if (dtype.Name == "Int32")
                            selectedValues = setValue2D<int>(indexes);
                        if (dtype.Name == "Single")
                            selectedValues = setValue2D<float>(indexes);
                        else if (dtype.Name == "Double")
                            selectedValues = setValue2D<double>(indexes);
                        break;

                    case 3:
                        if (dtype.Name == "Byte")
                            selectedValues = setValue3D<byte>(indexes);
                        if (dtype.Name == "Int32")
                            selectedValues = setValue3D<int>(indexes);
                        if (dtype.Name == "Single")
                            selectedValues = setValue3D<float>(indexes);
                        else if (dtype.Name == "Double")
                            selectedValues = setValue3D<double>(indexes);
                        break;

                    case 4:
                        if (dtype.Name == "Byte")
                            selectedValues = setValue4D<byte>(indexes);
                        if (dtype.Name == "Int32")
                            selectedValues = setValue4D<int>(indexes);
                        if (dtype.Name == "Single")
                            selectedValues = setValue4D<float>(indexes);
                        else if (dtype.Name == "Double")
                            selectedValues = setValue4D<double>(indexes);
                        break;
                }

                return selectedValues;
            }
        }

        private NDArray setValue1D<T>(NDArray indexes)
        {
            var buf = Data<T>();
            var idx = indexes.Data<int>();
            var values = new T[indexes.size];

            Parallel.For(0, indexes.size, (row) =>
            {
                values[row] = buf[idx[row]];
            });

            return new NDArray(values, indexes.size);
        }

        private NDArray setValue2D<T>(NDArray indexes)
        {
            var buf = Data<T>();
            var idx = indexes.Data<int>();
            var nd = new NDArray(dtype, new Shape(indexes.size, shape[1]));

            Parallel.For(0, nd.shape[0], (row) =>
            {
                for (int col = 0; col < nd.shape[1]; col++)
                    nd.SetData(buf[Storage.Shape.GetIndexInShape(idx[row], col)], row, col);
            });

            return nd;
        }

        private NDArray setValue3D<T>(NDArray indexes)
        {
            var buf = Data<T>();
            var nd = new NDArray(dtype, new Shape(indexes.size, shape[1], shape[2]));
            var idx = indexes.Data<int>();

            Parallel.For(0, nd.shape[0], (item) =>
            {
                for (int row = 0; row < nd.shape[1]; row++)
                    for (int col = 0; col < nd.shape[2]; col++)
                        nd.SetData(buf[Storage.Shape.GetIndexInShape(idx[item], row, col)], item, row, col);
            });

            return nd;
        }

        private NDArray setValue4D<T>(NDArray indexes)
        {
            var nd = new NDArray(dtype, new Shape(indexes.size, shape[1], shape[2], shape[3]));

            /*switch (Type.GetTypeCode(dtype))
            {
                case TypeCode.Byte:
                    {
                        var buf = Data<byte>().AsSpan();
                        
                        var idx = indexes.Data<int>().AsSpan();

                        var shapes = nd.shape.AsSpan();
                        var data = nd.Data<byte>().AsSpan();

                        Parallel.For(0, nd.shape[0], (item) =>
                        {
                            for (int row = 0; row < shapes[1]; row++)
                                for (int col = 0; col < shapes[2]; col++)
                                    for (int channel = 0; channel < shapes[3]; channel++)
                                        data[Storage.Shape.GetIndexInShape(item, row, col, channel)] = buf[Storage.Shape.GetIndexInShape(idx[item], row, col, channel)];
                        });
                    }
                    break;
            }*/

            {
                var buf = Data<T>();
                var idx = indexes.Data<int>();
                var data = nd.Data<T>();

                Parallel.For(0, nd.shape[0], (item) =>
                {
                    for (int row = 0; row < nd.shape[1]; row++)
                        for (int col = 0; col < nd.shape[2]; col++)
                            for (int channel = 0; channel < nd.shape[3]; channel++)
                                data[Storage.Shape.GetIndexInShape(item, row, col, channel)] = buf[Storage.Shape.GetIndexInShape(idx[item], row, col, channel)];
                });
            }

            return nd;
        }

        public NDArray this[NDArray<bool> booleanArray]
        {
            get
            {
                if (!Enumerable.SequenceEqual(shape,booleanArray.shape))
                {
                    throw new IncorrectShapeException();
                }

                var boolDotNetArray = booleanArray.Data<bool>();

                switch (dtype.Name)
                {
                    case "Int32":
                        {
                            var nd = new List<int>();

                            for (int idx = 0; idx < boolDotNetArray.Length; idx++)
                            {
                                if (boolDotNetArray[idx])
                                {
                                    nd.Add(Data<int>(booleanArray.Storage.Shape.GetDimIndexOutShape(idx)));
                                }
                            }

                            return new NDArray(nd.ToArray(), nd.Count);
                        }
                    case "Double":
                        {
                            var nd = new List<double>();

                            for (int idx = 0; idx < boolDotNetArray.Length; idx++)
                            {
                                if (boolDotNetArray[idx])
                                {
                                    nd.Add(Data<double>(booleanArray.Storage.Shape.GetDimIndexOutShape(idx)));
                                }
                            }

                            return new NDArray(nd.ToArray(), nd.Count);
                        }
                }

                throw new NotImplementedException("");

            }
            set
            {
                if (!Enumerable.SequenceEqual(shape,booleanArray.shape))
                {
                    throw new IncorrectShapeException();
                }

                object scalarObj = value.Storage.GetData().GetValue(0);

                bool[] boolDotNetArray = booleanArray.Storage.GetData() as bool[];

                int elementsAmount = booleanArray.size;

                for(int idx = 0; idx < elementsAmount;idx++)
                {
                    if (boolDotNetArray[idx])
                    {
                        int[] indexes = booleanArray.Storage.Shape.GetDimIndexOutShape(idx);
                        Array.SetValue(scalarObj, Storage.Shape.GetIndexInShape(indexes));
                    }
                }

            } 
        }
      
    }

    
}
