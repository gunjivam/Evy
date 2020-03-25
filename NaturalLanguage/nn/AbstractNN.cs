using System;
using System.Collections.Generic;
using System.Text;
using Tensorflow;

namespace NaturalLanguage.NN
{
    public class AbstractNN
    {
        public Config Config { get; set; }

        public virtual void BuildGraph()
        {
            throw new NotImplementedException();
        }

        public virtual Graph ImportGraph()
        {
            throw new NotImplementedException();
        }

        public virtual void PrepareData()
        {
            throw new NotImplementedException();
        }

        public virtual void Train()
        {

        }

        public virtual void Test()
        {

        }

        public virtual float[] Predict(string word)
        {
            throw new NotImplementedException();
        }

        public virtual string FreezeModel()
        {
            throw new NotImplementedException();
        }

        public virtual int GetOutputSize()
        {
            throw new NotImplementedException();
        }
    }
}
