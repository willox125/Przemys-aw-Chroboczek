using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Geolocation;
using System.Threading.Tasks;
using Windows.UI.Popups;
using System.Windows;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml.Shapes;
using Windows.Services.Maps;
using Windows.UI.Xaml.Documents;

namespace Przemysław_Chroboczek
{
    public sealed partial class MainPage : Page
    {
        //--- variables ---
        Geolocator geolocator = null;
        Geoposition geoposition = null;
        DispatcherTimer timer = new DispatcherTimer();
        bool tracking = false;
        private long startTime;
        string addressToGeocode;
        string totalTime;
        string empty = "";

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;

            MapService.ServiceToken = "AiQKDtms8jDCu5s-erQl4sPJK1Auy6E62NKTE-QUIrbP0fFeLamOlhY8LkUP_XHD";

            //--- one-second tick ---
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler<object>(timer_Tick);
        }


        
        protected override async void OnNavigatedTo(NavigationEventArgs e)                                                                  //OnNavigatedTo
        {
            myMap.MapElements.Clear();
            //--- set position ---
            geolocator = new Geolocator();
            try
            {
                geoposition = await geolocator.GetGeopositionAsync();
            }
            catch (Exception ex)
            {
                MessageDialog msgbox = new MessageDialog("Could not find your current location :( /n {0}", ex.Message);
                msgbox.ShowAsync();
            }
            await myMap.TrySetViewAsync(geoposition.Coordinate.Point, 17);

            //--- pin ---
            MapIcon mapIcon = new MapIcon()
            {
                NormalizedAnchorPoint = new Point(0.5, 1.0),
                Location = geoposition.Coordinate.Point,
            };
            myMap.MapElements.Add(mapIcon);
        }



        private void goButton_Click(object sender, RoutedEventArgs e)                                                                       //Buttons
        {
            dataField.Visibility = Visibility.Visible;
            goBarButton.IsEnabled = false;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            dataField.Visibility = Visibility.Collapsed;
            goBarButton.IsEnabled = true;
        }

        private async void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            string startAdress = fromTb.Text;
            addressToGeocode = toTb.Text;

            dataField.Visibility = Visibility.Collapsed;

            if (startAdress == "My location" || startAdress == "" || startAdress == " " || startAdress == "my location")
            {
                //--- hint ---
                Geolocator geolocator = new Geolocator();
                Geoposition hintPoint = await geolocator.GetGeopositionAsync();

                //--- result ---
                MapLocationFinderResult result = await MapLocationFinder.FindLocationsAsync(addressToGeocode, hintPoint.Coordinate.Point, 3);

                //--- if success ---
                if (result.Status == MapLocationFinderStatus.Success)
                {
                    //--- get route ---
                    MapRouteFinderResult routeResult = await MapRouteFinder.GetDrivingRouteAsync(hintPoint.Coordinate.Point, result.Locations[0].Point, MapRouteOptimization.Time, MapRouteRestrictions.None);

                    if (routeResult.Status == MapRouteFinderStatus.Success)
                    {
                        //--- initialize map route ---
                        MapRouteView viewOfRoute = new MapRouteView(routeResult.Route)
                        {
                            RouteColor = Colors.Blue,
                        };
                        myMap.Routes.Add(viewOfRoute);
                        await myMap.TrySetViewBoundsAsync(routeResult.Route.BoundingBox, null, Windows.UI.Xaml.Controls.Maps.MapAnimationKind.Bow);

                        estimatedTimeTb.Text = routeResult.Route.EstimatedDuration.TotalMinutes.ToString("F1");
                        lenghtTb.Text = (routeResult.Route.LengthInMeters / 1000).ToString("F1");

                        // Display summary info about the route.
                        turnByTurnTb.Text = empty;
                        turnByTurnTb.Inlines.Add(new Run()
                        {
                            Text = "Total estimated time (minutes) = " + routeResult.Route.EstimatedDuration.TotalMinutes.ToString("F1")
                        });
                        turnByTurnTb.Inlines.Add(new LineBreak());
                        turnByTurnTb.Inlines.Add(new Run()
                        {
                            Text = "Total length (kilometers) = " + (routeResult.Route.LengthInMeters / 1000).ToString("F1")
                        });
                        turnByTurnTb.Inlines.Add(new LineBreak());
                        turnByTurnTb.Inlines.Add(new LineBreak());

                        // Display the directions.
                        turnByTurnTb.Inlines.Add(new Run() { Text = "DIRECTIONS" });
                        turnByTurnTb.Inlines.Add(new LineBreak());
                        turnByTurnTb.Inlines.Add(new LineBreak());

                        // Loop through the legs and maneuvers.
                        int legCount = 0;
                        foreach (MapRouteLeg leg in routeResult.Route.Legs)
                        {
                            foreach (MapRouteManeuver maneuver in leg.Maneuvers)
                            {
                                turnByTurnTb.Inlines.Add(new Run() { Text = maneuver.InstructionText });
                                turnByTurnTb.Inlines.Add(new LineBreak());
                            }
                        }
                    }
                }
                else
                {
                    MessageDialog msgbox = new MessageDialog("Could not find a route");
                    msgbox.ShowAsync();
                }
                decisionField.Visibility = Visibility.Visible;
                directionsBarButton.IsEnabled = true;
            }
            else
            {
                // TODO
            }
        }

        private async void driveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!tracking)
            {
                myMap.MapElements.Clear();
                decisionField.Visibility = Visibility.Collapsed;
                tracking = true;
                
                geolocator = new Geolocator();
                geolocator.DesiredAccuracyInMeters = 5;

                geoposition = await geolocator.GetGeopositionAsync(TimeSpan.FromMinutes(5), TimeSpan.FromSeconds(5));

                var position = await geolocator.GetGeopositionAsync();
                await myMap.TrySetViewAsync(position.Coordinate.Point, 17);

                finishField.Visibility = Visibility.Visible;
                timer.Start();
                startTime = System.Environment.TickCount;

                //--- tracking <loop> ---
                do
                {
                    position = await geolocator.GetGeopositionAsync();
                    myMap.Center = position.Coordinate.Point;

                    var circle = new Ellipse()
                    {
                        Fill = new SolidColorBrush(Colors.Red),
                        Height = 10,
                        Width = 10,
                        Opacity = 50,
                    };
                    MapControl.SetLocation(circle, position.Coordinate.Point);
                    MapControl.SetNormalizedAnchorPoint(circle, new Point() { X = 0.5, Y = 0.7 });
                    myMap.Children.Add(circle);
                }
                while (tracking == true);
            }
            else
            {
                // TODO
            }
        }

        private async void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            myMap.Routes.Clear();
            myMap.MapElements.Clear();
            decisionField.Visibility = Visibility.Collapsed;
            directionsBarButton.IsEnabled = false;
            
            //--- set position ---
            geolocator = new Geolocator();
            geoposition = await geolocator.GetGeopositionAsync();
            await myMap.TrySetViewAsync(geoposition.Coordinate.Point, 17);

            //--- pin ---
            MapIcon mapIcon = new MapIcon()
            {
                NormalizedAnchorPoint = new Point(0.5, 1.0),
                Location = geoposition.Coordinate.Point,
            };
            myMap.MapElements.Add(mapIcon);

            goBarButton.IsEnabled = true;
        }

        private void finishButton_Click(object sender, RoutedEventArgs e)
        {
            tracking = false;
            timer.Stop();
            finishField.Visibility = Visibility.Collapsed;
            thanksField.Visibility = Visibility.Visible;
            totalTimeLabel.Text = totalTime;
            totalDistanceTb.Text = lenghtTb.Text;
        }

        private async void okButton_Click(object sender, RoutedEventArgs e)
        {
            thanksField.Visibility = Visibility.Collapsed;
            myMap.Routes.Clear();
            myMap.Children.Clear();
            directionsBarButton.IsEnabled = false;

            //--- set position ---
            geolocator = new Geolocator();
            geoposition = await geolocator.GetGeopositionAsync();
            await myMap.TrySetViewAsync(geoposition.Coordinate.Point, 17);

            //--- pin ---
            MapIcon mapIcon = new MapIcon()
            {
                NormalizedAnchorPoint = new Point(0.5, 1.0),
                Location = geoposition.Coordinate.Point,
            };
            myMap.MapElements.Add(mapIcon);

            goBarButton.IsEnabled = true;
        }



        private void directionsButton_Click(object sender, RoutedEventArgs e)                                                              //Bar Buttons
        {
            directionsField.Visibility = Visibility.Visible;
        }

        private void mapButton_Click(object sender, RoutedEventArgs e)
        {
            directionsField.Visibility = Visibility.Collapsed;
        }



        private async void myMap_MapTapped(MapControl sender, MapInputEventArgs args)                                                       //MapTapped
        {
            //--- hint ---
            Geolocator geolocator = new Geolocator();
            Geoposition hintPoint = await geolocator.GetGeopositionAsync();

            //--- result ---
            MapLocationFinderResult result = await MapLocationFinder.FindLocationsAtAsync(args.Location);

            //--- if success ---
            if (result.Status == MapLocationFinderStatus.Success)
            {
                //--- get route ---
                MapRouteFinderResult routeResult = await MapRouteFinder.GetDrivingRouteAsync(hintPoint.Coordinate.Point, result.Locations[0].Point, MapRouteOptimization.Time, MapRouteRestrictions.None);

                if (routeResult.Status == MapRouteFinderStatus.Success)
                {
                    //--- initialize map route ---
                    MapRouteView viewOfRoute = new MapRouteView(routeResult.Route)
                    {
                        RouteColor = Colors.Blue,
                    };
                    myMap.Routes.Add(viewOfRoute);
                    await myMap.TrySetViewBoundsAsync(routeResult.Route.BoundingBox, null, Windows.UI.Xaml.Controls.Maps.MapAnimationKind.Bow);

                    estimatedTimeTb.Text = routeResult.Route.EstimatedDuration.TotalMinutes.ToString("F1");
                    lenghtTb.Text = (routeResult.Route.LengthInMeters / 1000).ToString("F1");

                    // Display summary info about the route.
                    turnByTurnTb.Text = empty;
                    turnByTurnTb.Inlines.Add(new Run()
                    {
                        Text = "Total estimated time (minutes) = " + routeResult.Route.EstimatedDuration.TotalMinutes.ToString("F1")
                    });
                    turnByTurnTb.Inlines.Add(new LineBreak());
                    turnByTurnTb.Inlines.Add(new Run()
                    {
                        Text = "Total length (kilometers) = " + (routeResult.Route.LengthInMeters / 1000).ToString("F1")
                    });
                    turnByTurnTb.Inlines.Add(new LineBreak());
                    turnByTurnTb.Inlines.Add(new LineBreak());

                    // Display the directions.
                    turnByTurnTb.Inlines.Add(new Run() { Text = "DIRECTIONS" });
                    turnByTurnTb.Inlines.Add(new LineBreak());
                    turnByTurnTb.Inlines.Add(new LineBreak());

                    // Loop through the legs and maneuvers.
                    int legCount = 0;
                    foreach (MapRouteLeg leg in routeResult.Route.Legs)
                    {
                        foreach (MapRouteManeuver maneuver in leg.Maneuvers)
                        {
                            turnByTurnTb.Inlines.Add(new Run() { Text = maneuver.InstructionText });
                            turnByTurnTb.Inlines.Add(new LineBreak());
                        }
                    }
                }
            }
            decisionField.Visibility = Visibility.Visible;
            goBarButton.IsEnabled = false;
            directionsBarButton.IsEnabled = true;
        }



        private void road_Click(object sender, RoutedEventArgs e)                                                                       //Secondary Commands
        {
            myMap.Style = MapStyle.Road;
            nightButton.Label = "Night mode ON";
            myMap.ColorScheme = MapColorScheme.Light;
        }

        private void aerial_Click(object sender, RoutedEventArgs e)
        {
            myMap.Style = MapStyle.Aerial;
        }

        private void hybrid_Click(object sender, RoutedEventArgs e)
        {
            myMap.Style = MapStyle.AerialWithRoads;
        }

        private void dark_Click(object sender, RoutedEventArgs e)
        {
            if (myMap.ColorScheme == MapColorScheme.Light)
            {
                myMap.ColorScheme = MapColorScheme.Dark;
                nightButton.Label = "Night mode OFF";
            }
            else
            {
                myMap.ColorScheme = MapColorScheme.Light;
                nightButton.Label = "Night mode ON";
            }
        }

        private void landmarks_Click(object sender, RoutedEventArgs e)
        {
            if (!myMap.LandmarksVisible)
            {
                myMap.LandmarksVisible = true;
                landmarksButton.Label = "Landmarks OFF";
            }
            else
            {
                myMap.LandmarksVisible = false;
                landmarksButton.Label = "Landmarks ON";
            }
        }



        private void timer_Tick(object sender, object e)﻿                                                                               //Timer
        {
            TimeSpan runTime = TimeSpan.FromMilliseconds(System.Environment.TickCount - startTime);
            timeLabel.Text = runTime.ToString(@"hh\:mm\:ss");
            totalTime = runTime.ToString(@"hh\:mm\:ss");
        }
    }
}
