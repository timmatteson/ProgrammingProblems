/*
Blaine likes to deliberately crash toy trains!

Trains
Trains look like this

Aaaaaaaaaa
bbbB
The engine and carriages use the same character, but because the only engine is uppercase you can tell which way the train is going.

Trains can be any alphabetic character

An "Express" train uses X
Normal suburban trains are all other letters
Tracks
Track pieces are characters - | / \ + X and they can be joined together like this

Straights	
----------
 
|
|
|
\
 \
  \
   /
  /
 /
Corners	
|
|
\-----
     |
     |
-----/
/-----
|
|
-----\
     |
     |
Curves	
-----\
      \-----
      
      /-----
-----/

  |
  /
 /
 |
|
\
 \
 |
Crossings	
   |
---+---
   |
  \ /
   X
  / \
   /
---+---
   /
   | /
  /+/
 / |
Describing where a train is on the line
The track "zero position" is defined as the leftmost piece of track of the top row.

Other track positions are just distances from this zero position (following the line beginning clockwise).

A train position is the track position of the train engine.

Stations
Train stations are represented by a letter S.

Stations can be on straight sections of track, or crossings, like this

Stations	
----S-----


|
S
|
\
 S
  \
   /
  S
 /
    |
----S----
    |
 \ /
  S
 / \

When a train arrives at a station it stops there for a period of time determined by the length of the train!
The time T that a train will remain at the station is same as the number of carriages it has.

For example

bbbB - will stop at a station for 3 time units
Aa - will stop at a station for 1 time unit
Exception to the rule: The "Express" trains never stop at any station.

Collisions
There are lots of ways to crash trains. Here are a few of Blaine's favorites...

The Chicken-Run - Train chicken. Maximum impact.
The T-Bone - Two trains and one crossing
The Self-Destruct - Nobody else to blame here
The Cabooser - Run up the tail of a stopped train
The Kamikaze - Crash head-on into a stopped train
Kata Task
Blaine has a variety of continuous loop train lines.

Two trains are then placed onto the line, and both start moving at the same time.

How long (how many iterations) before the trains collide?

Input
track - string representation of the entire train line (\n separators - maybe jagged, maybe not trailing)
a - train A
aPos - train A start position
b - train B
bPos - train B start position
limit - how long before Blaine tires of waiting for a crash and gives up
Output
Return how long before the trains collide, or
Return -1 if they have not crashed before limit time has elapsed, or
Return 0 if the trains were already crashed in their start positions. Blaine is sneaky sometimes.
Notes
Trains

Speed...

All trains (even the "Express" ones) move at the same constant speed of 1 track piece / time unit

Length...

Trains can be any length, but there will always be at least one carriage

Stations...

Suburban trains stop at every station

"Express" trains don't stop at any station

If the start position happens to be at a station then the train leaves at the next move

Directions...

Trains can travel in either direction

A train that looks like zzzzzZ is travelling clockwise as it passed the track "zero position"

A train that looks like Zzzzzz is traveliing anti-clockwise as it passes the track "zero position"

Tracks

All tracks are single continuous loops

There are no ambiguous corners / junctions in Blaine's track layouts

All input is valid
https://www.codewars.com/kata/59b47ff18bcb77a4d1000076
*/

using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace CodeWarsCSharp
{
    /*
     * Driver code:
     * string track = @"/-----\   /-----\   /-----\   /-----\ 
|      \ /       \ /       \ /      | 
|       X         X         X       | 
|      / \       / \       / \      | 
\-----/   \-----/   \-----/   \-----/";



            TrainTracks tracks = new TrainTracks(track, "Xxxxxx", "Aaaaaaaaaaaaaaaa", 7, 0, 100);
            foreach (string l in tracks.Scene) { Console.WriteLine(l); }
            Console.WriteLine(string.Format("Moves: {0} Collided: {1} A Station Time: {2} B Station Time {3}", tracks.MoveCount, tracks.Collided, tracks.TrainAStationWait, tracks.TrainBStationWait));

            while (tracks.MoveCount <= tracks.Limit && !tracks.Collided)
            {
                tracks.AdvanceTrains();
                Console.Clear();
                foreach (string l in tracks.Scene) { Console.WriteLine(l); }
                Console.WriteLine(string.Format("Moves: {0} Collided: {1} A Station Time: {2} B Station Time {3}", tracks.MoveCount, tracks.Collided, tracks.TrainAStationWait, tracks.TrainBStationWait));
                System.Threading.Thread.Sleep(10);
            }
    */
    public class TrainTracks
    {
        private class TrackConnection
        {
            public Point Destination { get; set; }
            public Point PreviousOffset { get; }
            public Point DestinationOffset { get; }

            public string ValidDestinations { get; }

            public TrackConnection(Point previousOffset, Point destinationOffset, string validDestinations)
            {
                this.PreviousOffset = previousOffset;
                this.DestinationOffset = destinationOffset;
                this.ValidDestinations = validDestinations;
            }
            public TrackConnection(Point previousOffset, Point destinationOffset)
            {
                this.PreviousOffset = previousOffset;
                this.DestinationOffset = destinationOffset;
                this.ValidDestinations = @"-|/\+XS";
            }
        }

        private class TrackPiece
        {
            public List<TrackConnection> PathOffsets { get; } 
            public char Symbol { get; }

            public TrackPiece(List<TrackConnection> pathOffsets, char symbol)
            {
                this.PathOffsets = pathOffsets;
                this.Symbol = symbol;
            }

            public List<TrackConnection> GetPossiblePaths(Point origin, Point previous)
            {
                List<TrackConnection> results = new List<TrackConnection>();

                foreach (TrackConnection p in this.PathOffsets)
                {
                    TrackConnection connection = new TrackConnection(p.PreviousOffset, p.DestinationOffset, p.ValidDestinations);
                    Point possible = new Point();

                    if (GetPossiblePoint(origin, p.PreviousOffset, previous, p.DestinationOffset, ref possible))
                    {
                        connection.Destination = possible;
                        results.Add(connection);
                    }
                }

                return results;
            }

            private bool GetPossiblePoint(Point origin, Point previousOffset, Point previous, Point destinationOffset, ref Point possible)
            {
                Point test = new Point(origin.X, origin.Y);
                test.Offset(previousOffset);
                if (test == previous)
                {
                    possible = new Point(origin.X, origin.Y);
                    possible.Offset(destinationOffset);
                    return true;
                }
                return false;
            }
        }

        private Point selfOffset = new Point(0, 0);
        private Point westOffset = new Point(-1, 0);
        private Point eastOffset = new Point(1, 0);
        private Point northOffset = new Point(0, -1);
        private Point southOffset = new Point(0, 1);
        private Point northEastOffset = new Point(1, -1);
        private Point southWestOffset = new Point(-1, 1);
        private Point northWestOffset = new Point(-1, -1);
        private Point southEastOffset = new Point(1, 1);

        public string[] Track { get; }
        public string[] Scene { get; private set; }
        public string TrainA { get; }
        public string TrainB { get; }
        public int TrainAPosition { get; private set; }
        public int TrainBPosition { get; private set; }

        public int TrainAStationWait { get { return this.TrainAStationTime; } }
        public int TrainBStationWait { get { return this.TrainBStationTime; } }

        public int Limit { get; }

        public bool Collided { get; private set; }

        public int MoveCount { get; private set; } = 1;
        private int TrackLength { get; set; }

        private int TrainAStationTime;
        private int TrainBStationTime;

        private Dictionary<int, Point> trackMap;
        private List<TrackPiece> TrackPieces = new List<TrackPiece>();


        public TrainTracks(string track, string trainA, string trainB, int aPos, int bPos, int limit)
        {
            this.Track = track.Split(Environment.NewLine);
            this.TrainA = trainA;
            this.TrainB = trainB;
            this.TrainAPosition = aPos;
            this.TrainBPosition = bPos;
            this.Limit = limit;
            
            TrackPieces.Add(new TrackPiece(new List<TrackConnection> { new TrackConnection(eastOffset, westOffset),
                                                                       new TrackConnection(westOffset, eastOffset),
                                                                          new TrackConnection(selfOffset, westOffset) }, '-'));

            TrackPieces.Add(new TrackPiece(new List<TrackConnection>{new TrackConnection(northOffset, southOffset),
                                                                     new TrackConnection(southOffset, northOffset),
                                                                          new TrackConnection(selfOffset, southOffset)}, '|'));

            TrackPieces.Add(new TrackPiece(new List<TrackConnection>{new TrackConnection(northOffset, eastOffset, @"-S+"),
                                                                          new TrackConnection(northOffset, southEastOffset, @"\SX"),
                                                                          new TrackConnection(northWestOffset, eastOffset, @"-S+"),
                                                                          new TrackConnection(northWestOffset, southOffset, @"|S+"),
                                                                          new TrackConnection(northEastOffset, northOffset, @"\SX"),
                                                                          new TrackConnection(northWestOffset, southEastOffset, @"\SX"),
                                                                          new TrackConnection(eastOffset, northOffset, @"|S+"),
                                                                          new TrackConnection(eastOffset, northWestOffset, @"\SX"),
                                                                          new TrackConnection(westOffset, southOffset, @"|S+"),
                                                                          new TrackConnection(southOffset, northWestOffset, @"\SX"),
                                                                          new TrackConnection(southOffset, westOffset, @"-"),
                                                                          new TrackConnection(southEastOffset, northWestOffset, @"\SX"),
                                                                          new TrackConnection(southEastOffset, northOffset, @"|S+"),
                                                                          new TrackConnection(southEastOffset, westOffset, @"-S+"),
                                                                          new TrackConnection(westOffset, southEastOffset, @"\SX")}, '\\'));

            TrackPieces.Add(new TrackPiece(new List<TrackConnection>{new TrackConnection(northOffset, westOffset, @"-S+"),
                                                                     new TrackConnection(westOffset, northOffset, @"S+|"),
                                                                     new TrackConnection(westOffset, northEastOffset, @"/SX"),
                                                                          new TrackConnection(northOffset, southWestOffset, @"/SX"),
                                                                          new TrackConnection(southWestOffset, eastOffset, @"-S+"),
                                                                          new TrackConnection(northEastOffset, westOffset, @"-S+"),
                                                                          new TrackConnection(northEastOffset, southOffset, @"|"),
                                                                          new TrackConnection(northEastOffset, southWestOffset, @"/SX"),
                                                                          new TrackConnection(eastOffset, southOffset, @"|S+"),
                                                                          new TrackConnection(southOffset, eastOffset, @"S+-"),
                                                                          new TrackConnection(southOffset, northEastOffset, @"/SX"),
                                                                          new TrackConnection(southWestOffset, northEastOffset, @"/SX"),
                                                                          new TrackConnection(eastOffset, southWestOffset, @"/SX"),
                                                                          new TrackConnection(selfOffset, eastOffset, @"-S+")}, '/'));

            TrackPieces.Add(new TrackPiece(new List<TrackConnection>{new TrackConnection(northOffset, southOffset),
                                                                     new TrackConnection(southOffset, northOffset),
                                                                     new TrackConnection(westOffset, eastOffset),
                                                                          new TrackConnection(eastOffset, westOffset)}, '+'));

            TrackPieces.Add(new TrackPiece(new List<TrackConnection>{new TrackConnection(northEastOffset, southWestOffset),
                                                                          new TrackConnection(northWestOffset, southEastOffset),
                                                                          new TrackConnection(southWestOffset, northEastOffset),
                                                                          new TrackConnection(southEastOffset, northWestOffset)}, 'X'));

            TrackPieces.Add(new TrackPiece(new List<TrackConnection>{new TrackConnection(northEastOffset, southWestOffset),
                                                                          new TrackConnection(northWestOffset, southEastOffset),
                                                                          new TrackConnection(southWestOffset, northEastOffset),
                                                                          new TrackConnection(southEastOffset, northWestOffset),
                                                                          new TrackConnection(northOffset, southOffset),
                                                                          new TrackConnection(southOffset, northOffset),
                                                                          new TrackConnection(westOffset, eastOffset),
                                                                          new TrackConnection(eastOffset, westOffset)}, 'S'));

            this.MapTrack();
            Collided = this.RenderTrains();
        }

        public void AdvanceTrains()
        {
            this.TrainAPosition = AdvanceTrain(this.TrainA, this.TrainAPosition, ref this.TrainAStationTime);
            this.TrainBPosition = AdvanceTrain(this.TrainB, this.TrainBPosition, ref this.TrainBStationTime);

            Collided = this.RenderTrains();
            this.MoveCount++;
        }

        public bool RenderTrains()
        {
            string[] buffer = this.GetBuffer();

            List<Point> Atrain = RenderTrain(this.TrainA, this.TrainAPosition, buffer);
            List<Point> Btrain = RenderTrain(this.TrainB, this.TrainBPosition, buffer);
            this.Scene = buffer;

            bool collision = (Atrain.Intersect(Btrain).Count() > 0);
            collision |= (Atrain.Distinct().Count() < Atrain.Count());
            collision |= (Btrain.Distinct().Count() < Btrain.Count());

            return collision;
        }

        private int AdvanceTrain(string train, int position, ref int stationCount)
        {
            Point currentPoint = trackMap[position];
            int direction = (char.IsUpper(train[0]) ? -1 : 1);
            bool isExpress = (train.ToLower()[0] == 'x');
            bool inStation = (GetCharAtPosition(currentPoint) == 'S' && this.MoveCount != 1);
            int newPosition = position + direction;

            if (newPosition < 0) newPosition = this.TrackLength;
            if (newPosition > this.TrackLength) newPosition = 0;

            if (!isExpress && inStation)
            {
                stationCount++;
                if (stationCount >= train.Length)
                {
                    stationCount = 0;
                }
                else
                {
                    return position;
                }
            }
            return newPosition;
        }

        private List<Point> RenderTrain(string train, int position, string[] buffer)
        {
            var start = this.trackMap[position];
            int direction = (char.IsUpper(train[0]) ? 1 : -1);
            List<Point> trainPosition = new List<Point>();
            char trainCar = train[0].ToString().ToLower()[0];
            char trainCarriage = train[0].ToString().ToUpper()[0];

            int cnt = 0;
            int curPosition = position;

            while (cnt < train.Length)
            {
                Point next = trackMap[curPosition];
                trainPosition.Add(next);
                SetCharAtPosition(buffer, next, (curPosition == position ? trainCarriage : trainCar));
                cnt++;
                curPosition += direction;
                if (curPosition < 0) curPosition = this.TrackLength;
                if (curPosition > this.TrackLength) curPosition = 0;
            }
            return trainPosition;
        }

        private void MapTrack()
        {
            trackMap = new Dictionary<int, Point>();

            Point start = FindStart();
            Point current = start;
            Point previous = start;
            Point next = new Point(-1, -1);
            int cnt = 0;

            do
            {
                next = MapNextTrack(current, previous);
                trackMap.Add(cnt, current);
                cnt++;
                previous = current;
                current = next;
                //string[] test = GetBuffer();
                //SetCharAtPosition(test, current, '*');
                //Console.Clear();
                //foreach (string l in test) { Console.WriteLine(l); }
                //Console.WriteLine();
            } while (current != start && cnt < 100000);

            this.TrackLength = cnt - 1;
        }

        private Point MapNextTrack(Point currentTrack, Point lastTrack)
        {
            char? current = GetCharAtPosition(currentTrack);
            Point result = new Point(-1, -1);

            var trackPiece = TrackPieces.FirstOrDefault(x => x.Symbol == current.Value);
            var points = trackPiece.GetPossiblePaths(currentTrack, lastTrack);

            foreach (TrackConnection p in points)
            {
                char? test = GetCharAtPosition(p.Destination);
                if (IsTrack(test) && p.ValidDestinations.Contains(test.Value))
                {
                    return p.Destination;
                }
            }

            return result;
        }

        private string[] GetBuffer()
        {
            string[] buffer = new string[this.Track.GetUpperBound(0) +  1];

            this.Track.CopyTo(buffer, 0);
            return buffer;
        }

        private Point FindStart()
        {
            Point result = new Point(0, 0);
            char? track = null;

            do
            {
                track = GetCharAtPosition(result);
                if (IsTrack(track))
                    return result;

            } while (MoveToNextPoint(ref result));

            throw new Exception("No starting point found, invalid track.");
        }

        private char? GetCharAtPosition(Point point)
        {
            if (point.Y >= 0 && point.Y <= Track.GetUpperBound(0) && point.X < Track[point.Y].Length && point.X >= 0)
            {
                return Track[point.Y].ElementAt(point.X);
            }
            else
            {
                return null;
            }
        }

        private void SetCharAtPosition(string[] buffer, Point point, char character)
        {
            if (point.Y <= buffer.GetUpperBound(0) && point.X < buffer[point.Y].Length)
            {
                string temp = buffer[point.Y];
                buffer[point.Y] = temp.Substring(0, point.X) + character + temp.Substring(point.X + 1, temp.Length - point.X - 1);
            }
        }

        private bool MoveToNextPoint(ref Point currentPoint)
        {
            if (currentPoint.X < Track[currentPoint.Y].Length)
                currentPoint.X++;
            else if (currentPoint.Y < Track.GetUpperBound(0))
            {
                currentPoint.X = 0;
                currentPoint.Y++;
            }
            else
                return false;

            return true;
        }

        private bool IsTrack(char? test)
        {
            //A train is also a track, assuming a train cannot exist off of the track in our map
            return test.HasValue &&  (@"-|/\+XS".Contains(test.Value) || IsTrain(test.Value));
        }

        private bool IsTrain(char? test)
        {
            return test.HasValue && (TrainA.Contains(test.Value) || TrainB.Contains(test.Value));
        }

    }
}
