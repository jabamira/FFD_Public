using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows;

namespace FastFoodDelivery
{
    public static class Animations
    {
        public static DoubleAnimation CreateScaleAnimationStandartIn()
        {
            return new DoubleAnimation
            {
                From = 1,
                To = 1.1,
                Duration = TimeSpan.FromMilliseconds(100),
                AutoReverse = false,
                EasingFunction = new QuadraticEase
                {
                    EasingMode = EasingMode.EaseInOut
                }
            };
        }

        public static DoubleAnimation CreateScaleAnimationStandartOut()
        {
            return new DoubleAnimation
            {
                From = 1.1,
                To = 1.0,
                Duration = TimeSpan.FromMilliseconds(100),
                AutoReverse = false,
                EasingFunction = new QuadraticEase
                {
                    EasingMode = EasingMode.EaseInOut
                }
            };
        }

        public static void StartAnimation(DoubleAnimation animation, object Anim_Obj, bool UseMargin=true, byte SpecAnim = 0)
        {
            ScaleTransform scaleTransform = new ScaleTransform();

            if (Anim_Obj is FrameworkElement Anim)
            {
                Anim.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
                Anim.RenderTransform = scaleTransform;

                // Получаем текущий Margin
                Thickness currentMargin = Anim.Margin;

                if (UseMargin) 
                {
                    animation.Completed += (s, e) =>
                    {
                        if (animation.To == 1.1)
                        {
                            // Увеличиваем Margin на 10%
                            Anim.Margin = new Thickness(
                                currentMargin.Left * 1.1,
                                currentMargin.Top * 1.1,
                                currentMargin.Right * 1.1,
                                currentMargin.Bottom * 1.1
                            );

                            // Запускаем анимацию выхода
                           // StartAnimation(CreateScaleAnimationStandartOut(), Anim_Obj);
                        }
                        else if (animation.To == 1.0)
                        {
                            // Уменьшаем Margin обратно на 10%
                            Anim.Margin = new Thickness(
                                currentMargin.Left / 1.1,
                                currentMargin.Top / 1.1,
                                currentMargin.Right / 1.1,
                                currentMargin.Bottom / 1.1
                            );
                        }
                    };
                }
            }
                

            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }

    }
}
