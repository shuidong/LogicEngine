//======================================================
// Create by @Peng Guang Hui
// 2015/11/2 16:33:07
//======================================================
using System;
using System.Collections.Generic;

namespace LogicEngine
{
    class SnowflakeID
    {
        readonly int sequenceBitCount = 12;
        readonly int alloterBitCount = 10;
        readonly int timestampBitCount = 41;
        readonly long sequenceMax = 1 << 12;

        long alloterID;
        long lastTimestamp;
        long acc = 0;

        public SnowflakeID(long alloterID)
        {
            if (0 > alloterID || alloterID >= 1 << alloterBitCount)
            {
                throw new ArgumentOutOfRangeException("alloterID", alloterID, "设备ID范围:[0, " + (1 << alloterBitCount) + ")");
            }
            this.alloterID = alloterID << sequenceBitCount;
        }

        public long GenID()
        {
            var s = GetSequence();
            var t = lastTimestamp << sequenceBitCount + alloterBitCount;
            return t | alloterID | s;
        }
        public long GetTimestamp()
        {
            return DateTime.UtcNow.Ticks / 1000 - 621355968000000L;
        }

        long GetSequence()
        {
            var now = GetTimestamp();
            if (lastTimestamp == now)
            {
                acc++;
                if (acc == sequenceMax)
                {
                    do
                    {
                        now = GetTimestamp();
                    } while (lastTimestamp == now);
                    lastTimestamp = now;
                    acc = 0;
                }
                return acc;
            }
            else
            {
                lastTimestamp = now;
                acc = 0;
                return acc;
            }
        }
    }
}