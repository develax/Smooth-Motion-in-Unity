# Smooth Motion in Unity (improved)

This is an improved (refactored) version of the code from the ["Timesteps and Achieving Smooth Motion in Unity"](https://www.kinematicsoup.com/news/2016/8/9/rrypp5tkubynjwxhxjzd42s3o034o8?utm_source=youtube&utm_type=SMVideo) article.

Now, there are only 2 components instead of original 3:

1. `InterpolationFactorController` a singleton scene component (the original name was `InterpolationController`) that calculates the current *interpolation factor*.
2. `InterpolationObjectController` a component for a moving object (the original name was `InterpolatedTransform`) which calculates the current interpolation for the object in `Update()` method before it's being rendering and then restores original values for the next `FixedUpdate()` call.
3. The `InterpolatedTransformUpdater` component was removed as unnecessary.

## Remarks
For these scripts to work correctly the user's script must change the object's `transform` only in the `FixedUpdate()` method. When teleporting the object the user should call the (`InterpolationObjectController`).`ResetTransforms()` method.
