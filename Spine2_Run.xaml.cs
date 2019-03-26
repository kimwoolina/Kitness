using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Kinect;

namespace Kitness1
{
    /// <summary>
    /// Leg2_Run.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Spine2_Run : Window
    {
        Rectangle[] m_rect = new Rectangle[20];
        //Rectangle 상자 그리는 함수를 표시할 사각형만큼 배열 선언
        //상자는 관절을 표현하는 위치를 보여주는 것이므로 관절
        //즉 조인트는 키넥트가 관절을 총 20개로 인식하는 것에 따라 크기가 20

        Polyline[] m_poly = new Polyline[5];
        //Polyline 선 그리는 함수를 표시할 선만큼 배열 선언
        //머리-엉덩이, 왼쪽 팔, 왼쪽 발, 오른쪽 팔, 오른쪽 발

        public Spine2_Run()
        {
            InitializeComponent();
            InitializeNui();
            //키넥트를 사용하기 위한 초기화 및 설정을 진행하기 위해 따로 뺀 메소드

            for (int i = 0; i < 20; i++)
            { //상자를 그리기위해 선언했으니 상자를 생성(초기화 및 설정)
                m_rect[i] = new Rectangle();
                m_rect[i].Fill = new SolidColorBrush(Colors.Red);
                m_rect[i].Height = 10;
                m_rect[i].Width = 10;
                m_rect[i].Visibility = Visibility.Collapsed;
                Canvas.SetTop(m_rect[i], 0);
                Canvas.SetLeft(m_rect[i], 0);
                canvas1.Children.Add(m_rect[i]);
            }

            for (int i = 0; i < 5; i++)
            { //선을 그리기 위해 선언했으니 선을 생성(초기화 및 설정)
                m_poly[i] = new Polyline();
                m_poly[i].Stroke = new SolidColorBrush(Colors.Green);
                m_poly[i].StrokeThickness = 4;
                m_poly[i].Visibility = Visibility.Collapsed;
                Canvas.SetTop(m_poly[i], 0);
                Canvas.SetLeft(m_poly[i], 0);
                canvas1.Children.Add(m_poly[i]);
            }
        }

        KinectSensor nui = null;
        //키넥트를 사용하기위한 필드 선언
        //쉽게 키넥트의 센서를 이용하기위한 변수를 선언한 것

        void InitializeNui()
        {//키넥트를 사용하기 위한 초기화 및 설정
            nui = KinectSensor.KinectSensors[0];
            //사용하는 키넥트가 1개이므로 사전에 0번 배열로 초기화 시킴

            nui.ColorStream.Enable();
            //일반 카메라로 본 이미지를 컴퓨터에 뿌려주는 기능(colorstream)을 활성화 시킴
            nui.ColorFrameReady += new EventHandler<ColorImageFrameReadyEventArgs>(nui_ColorFrameReady);
            //활성화 시킨 기능(colorstream)에 추가한 이벤트 핸들러(처리) 추가

            nui.DepthStream.Enable();
            //물체와 센서간의 거리를 측정할 수 있는 기능(depthstream)을 활성화 시킴
            nui.SkeletonStream.Enable();
            //스캘리톤(골격정보)을 처리하기 위해 활성화
            nui.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(nui_AllFramesReady);
            //활성화 시킨 기능(DepthStream, SkeletonStream)을 돌릴 이벤트 핸들러 추가
            //두 개가 서로 사용비율이 높아 하나로 사용할 수 있도록 이벤트 핸들러를 하나로 함

            nui.Start();
            //활성화 시켰으니 받은 것 시작
        }

        void nui_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        { //colorstream에 추가한 이벤트 핸들러
            ColorImageFrame ImageParam = e.OpenColorImageFrame();
            //현재 인식한 이미지(카메라로 보이는 화면)을 ImageParm에 저장
            if (ImageParam == null) return;
            //인식된 이미지가 없으면 사람이 보이지도 않으므로 종료

            byte[] ImageBits = new byte[ImageParam.PixelDataLength];
            //각각의 frame을 저장 > 보여주는 화면은 정지된 이미지가 아닌 움직이는 화면이니까
            ImageParam.CopyPixelDataTo(ImageBits);

            BitmapSource src = null;
            //화면에 보여줄 이미지 폭과 너비, 가로, 세로, 어떤 픽셀 형식을 쓸건지 등등 저장할 변수
            src = BitmapSource.Create(ImageParam.Width, ImageParam.Height, 96, 96, PixelFormats.Bgr32,
                                      null, ImageBits, ImageParam.Width * ImageParam.BytesPerPixel);
            //변수에 화면에 보여줄 이미지 정보를 넣는다
            user_img.Source = src;
            //화면을 화면에 보여줄 이미지를 저장한 변수로 지정 >> 변수 정보가 화면에 뿌려짐
        }

        private void nui_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        { //DepthStream과 SkeletonStream에 동시에 추가한 이벤트 핸들러 
            SkeletonFrame sf = e.OpenSkeletonFrame();
            //현재 인식한 골격정보(스캘리톤)을 sf라는 변수에 저장
            if (sf == null) return;
            //골격이 인식되지 못하면, 즉 사람이 인식되지 않으면 보여줄 상자, 이를 이은 선을 보여줄 필요가 없으므로 종료
            Skeleton[] skeletonData = new Skeleton[sf.SkeletonArrayLength];
            //스캘리톤(골격정보)를 저장하라 배열을 인식된 수만큼 크기 지정
            sf.CopySkeletonDataTo(skeletonData);
            //현재 SkeletonFrame안에서 스캘리톤 데이터를 복사
            using (DepthImageFrame depthImageFrame = e.OpenDepthImageFrame())
            { //현재 DepthImageFrame 안에서 데이터 복사
                if (depthImageFrame != null)
                { //데이터가 있다면
                    foreach (Skeleton sd in skeletonData)
                    { //복사해온 스캘리톤 데이터를 하나씩 차근차근 반복한다
                        if (sd.TrackingState == SkeletonTrackingState.Tracked)
                        { //인식된 사용자 인덱스 중 추적이 되는 Tracked에 해당하는 골격정보만 사용
                            int nMax = 20; //키넥트가 인식하는 관절 포인트가 20개 이므로

                            Joint[] joints = new Joint[nMax];
                            //만들 조인트만큼(키넥트가 인식하는 것만큼> 20) 조인트 배열 선언
                            for (int j = 0; j < nMax; j++)
                            { //조인트 생성(초기화 및 설정)
                                joints[j] = sd.Joints[(JointType)j];
                            }
                            //여기까지 찾아진 골격상태에서 각 골격의 정보를 얻어오기 위함

                            Point[] points = new Point[nMax];
                            //뼈대 위치를 저장할 배열
                            for (int j = 0; j < nMax; j++)
                            {
                                DepthImagePoint depthPoint;
                                depthPoint = depthImageFrame.MapFromSkeletonPoint(joints[j].Position);
                                points[j] = new Point((int)((user_img.Width * depthPoint.X / depthImageFrame.Width) + 340),
                                                        (int)(user_img.Height * depthPoint.Y / depthImageFrame.Height));
                            }
                            //여기까지 각 조인트정보로부터 좌표를 얻기 위함
                            
                            for (int j = 0; j < nMax; j++)
                            {
                                m_rect[j].Visibility = Visibility.Visible;
                                //아까 만들었던 사각형을 보이도록 함
                                Canvas.SetTop(m_rect[j],
                                              points[j].Y - (m_rect[j].Height / 2));
                                Canvas.SetLeft(m_rect[j], points[j].X - (m_rect[j].Width / 2));
                                //사각형 배치
                            }
                            //여기까지 가져온 각 조인트의 정보를 화면에 표시하는 코드
                            

                            //엉덩이 중앙부터 머리까지 연결되는 선을 긋는 코드
                            PointCollection pc0 = new PointCollection(4);
                            //이을 관절 개수만큼 크기를 지정하고 위치를 저장할  변수
                            pc0.Add(points[(int)JointType.HipCenter]); //엉덩이 중간
                            pc0.Add(points[(int)JointType.Spine]); //등뼈
                            pc0.Add(points[(int)JointType.ShoulderCenter]); //어깨 중간
                            pc0.Add(points[(int)JointType.Head]); //머리
                            m_poly[0].Points = pc0; //관절들 위치를 저장한 변수를 선으로 그리는 함수로 선언한 배열에 넣는다
                            m_poly[0].Visibility = Visibility.Visible;
                            //넣었으니 활성화시켜 위치를 토대로 그린 선을 보여준다


                            //왼쪽 손부터 어깨까지 연결되는 선을 긋는 코드
                            PointCollection pc1 = new PointCollection(5);
                            pc1.Add(points[(int)JointType.ShoulderCenter]); //어깨 중간
                            pc1.Add(points[(int)JointType.ShoulderLeft]); //왼쪽 어깨
                            pc1.Add(points[(int)JointType.ElbowLeft]); //왼쪽 팔꿈치
                            pc1.Add(points[(int)JointType.WristLeft]); //왼쪽 손목
                            pc1.Add(points[(int)JointType.HandLeft]); //왼손
                            m_poly[1].Points = pc1;
                            m_poly[1].Visibility = Visibility.Visible;


                            //오른쪽 손부터 어깨까지 연결되는 선을 긋는 코드
                            PointCollection pc2 = new PointCollection(5);
                            pc2.Add(points[(int)JointType.ShoulderCenter]); //어깨 중간
                            pc2.Add(points[(int)JointType.ShoulderRight]); //오른쪽 어깨
                            pc2.Add(points[(int)JointType.ElbowRight]); //오른쪽 팔꿈치
                            pc2.Add(points[(int)JointType.WristRight]); //오른쪽 손목
                            pc2.Add(points[(int)JointType.HandRight]); //오른손
                            m_poly[2].Points = pc2;
                            m_poly[2].Visibility = Visibility.Visible;


                            //왼발부터 엉덩이까지 연결되는 선을 긋는 코드
                            PointCollection pc3 = new PointCollection(5);
                            pc3.Add(points[(int)JointType.HipCenter]); //엉덩이 중간
                            pc3.Add(points[(int)JointType.HipLeft]); //엉덩이 왼쪽
                            pc3.Add(points[(int)JointType.KneeLeft]); //왼쪽 무릎
                            pc3.Add(points[(int)JointType.AnkleLeft]); //왼쪽 발목
                            pc3.Add(points[(int)JointType.FootLeft]); //왼쪽 발
                            m_poly[3].Points = pc3;
                            m_poly[3].Visibility = Visibility.Visible;


                            //오른발부터 엉덩이까지 연결되는 선을 긋는 코드
                            PointCollection pc4 = new PointCollection(5);
                            pc4.Add(points[(int)JointType.HipCenter]); //엉덩이 중간
                            pc4.Add(points[(int)JointType.HipRight]); //엉덩이 오른쪽
                            pc4.Add(points[(int)JointType.KneeRight]); //오른쪽 무릎
                            pc4.Add(points[(int)JointType.AnkleRight]); //오른쪽 발목
                            pc4.Add(points[(int)JointType.FootRight]); //오른쪽 발
                            m_poly[4].Points = pc4;
                            m_poly[4].Visibility = Visibility.Visible;


                        }
                    }
                }
            }
        }

        private void btBack_Click(object sender, RoutedEventArgs e)
        {
            Spine2 spine2 = new Spine2(); //창 전환
            Close();
            spine2.ShowDialog();
        }
    }
}
