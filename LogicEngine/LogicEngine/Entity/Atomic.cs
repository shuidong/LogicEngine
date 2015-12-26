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
using System.Text;
using System.IO;

namespace LogicEngine
{
    public enum AtomicType : int
    {
        Login = -1,
    }

    public class Atomic
    {
        public long Key;
        public long By;
        public long Type;
        public string Text;
    }

    public class AtomicDao
    {
        List<Atomic> atomics = new List<Atomic>();

        public void Load(string file)
        {
            using (StringReader reader = new StringReader(UtilFile.LoadText(file, "txt")))
            {
                atomics.Clear();

                string line = reader.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    Atomic atomic = UtilJson.To<Atomic>(line);
                    if(atomic != null)
                    {
                        atomics.Add(atomic);
                    }
                    line = reader.ReadLine();
                } 
            }
        }
        public void Save(string file)
        {
            using (StreamWriter writer = new StreamWriter(Directory.GetCurrentDirectory() + file, false))
            {
                foreach (var it in atomics)
                {
                    writer.WriteLine(UtilJson.ToJson(it));
                }
            }
        }

        public Atomic Select(long key)
        {
            return atomics.Find(it => it.Key == key);
        }
        public List<Atomic> SelectBy(long by)
        {
            return atomics.FindAll(it => it.By == by);
        }
        public List<Atomic> SelectType(AtomicType type)
        {
            return atomics.FindAll(it => it.Type == (int)type);
        }
        public void Insert(long key, long by, long type, string text)
        {
            Atomic atomic = new Atomic();
            atomic.Key = key;
            atomic.By = by;
            atomic.Type = type;
            atomic.Text = text;
            atomics.Add(atomic);
        }
    }
}